using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class PrintJobPureGoldViewModelBuilder
    {

        public static IEnumerable<PrintJobPureGoldViewModel> GetViewModels
            (Int32 printJobId, IEnumerable<Int32> printJobAppObjectToTransactionIds, IToolboxService toolboxService)
        {
            var viewModel = new List<PrintJobPureGoldViewModel>();

            var printJobToAppObjectToTransactions = 
                toolboxService.PrintJobService.GetPrintJobAppObjectToTransactionsByIds(printJobId, printJobAppObjectToTransactionIds).ToList();            

            foreach (var printJobToAppObjectToTransaction in printJobToAppObjectToTransactions)
            {
                var localTransaction = printJobToAppObjectToTransaction.AppObjectToTransaction.Transaction;
                //var appObjectToTransactionId = printJobToAppObjectToTransaction.AppObjectToTransactionId;
                                
                var getPropertyTask = Task.Factory.StartNew(() => 
                    toolboxService.AtlasXService.GetPropertyById(localTransaction.AtlasXPropertyId));
                var getPropertyAltTask = getPropertyTask.ContinueWith((t) => 
                    toolboxService.AtlasXService.GetPropertyAlternateById(localTransaction.AtlasXPropertyAlternateId));
                
                var taskAgentViewModels = Task.Factory.StartNew(() => 
                    AgentViewModelBuilder.GetViewModels(new List<Transaction> { localTransaction }, toolboxService));

                //var getMailingTask = taskAgentViewModels.ContinueWith((t) => 
                //    toolboxService.PrintJobService.GetPureGoldMailing(appObjectToTransactionId));

                //Task.WaitAll(getMailingTask, getPropertyAltTask, taskAgentViewModels);
                Task.WaitAll(getPropertyAltTask, taskAgentViewModels);

                viewModel.Add(new PrintJobPureGoldViewModel
                {
                    PureGoldMailings = printJobToAppObjectToTransaction.AppObjectToTransaction.PureGoldMailings,
                    AppObjectToTransaction = printJobToAppObjectToTransaction.AppObjectToTransaction,
                    PrintJobToAppObjectToTransaction = printJobToAppObjectToTransaction,
                    Transaction = localTransaction,
                    Property = getPropertyTask.Result,
                    PropertyAlternate = getPropertyAltTask.Result,
                    AgentViewModels = taskAgentViewModels.Result
                });
            }

            return viewModel;
        }

        public static IEnumerable<PrintJobPureGoldViewModel> GetViewModels
            (IEnumerable<Int32> printJobToWeichertSLIds, IToolboxService toolboxService)
        {
            var viewModel = new List<PrintJobPureGoldViewModel>();

            // Get PrintJobToWeichertSLs
            var printJobToWeichertSLs = toolboxService.PrintJobService
                .GetPrintJobToWeichertSLsByPrintJobToWeichertSLIds(printJobToWeichertSLIds);

            foreach (var printJobToWeichertSL in printJobToWeichertSLs)
            {
                var list = toolboxService.WeichertSLService
                    .GetListBySaleId(printJobToWeichertSL.SaleId);

                var sale = list.Sales
                    .Single(s => s.SaleId == printJobToWeichertSL.SaleId);

                var customerNames = sale.SaleToBuyers
                    .Select(saleToBuyer => saleToBuyer.LastName.Trim() + ", " + saleToBuyer.FirstName.Trim()).ToList();

                var relationshipNumbers = sale.SaleToAssociates
                    .Select(a => a.RelationshipNumber);

                var taskAgentViewModels = Task.Factory
                    .StartNew(() => AgentViewModelBuilder.GetViewModels(relationshipNumbers, toolboxService));

                Task.WaitAll(taskAgentViewModels);

                viewModel.Add(new PrintJobPureGoldViewModel
                {
                    PrintJobToWeichertSL = printJobToWeichertSL,
                    Property = new Property
                    {
                        PropertyId = list.ListProperty.AtlasXPropertyId ?? 440000000
                    },
                    PropertyAlternate = new PropertyAlternate
                    {
                        AltAddress1 = list.ListProperty.Address1,
                        AltAddress2 = list.ListProperty.Address2,
                        AltCity = list.ListProperty.City,
                        AltState = list.ListProperty.State,
                        AltZip = list.ListProperty.ZipCode
                    },
                    AgentViewModels = taskAgentViewModels.Result,
                    CustomerNames = customerNames
                });
            }
            return viewModel;
        }

        public static IEnumerable<PrintJobPureGoldViewModel> GetViewModels
            (Int32 printJobId, IEnumerable<AppObjectToTransaction> appObjectToTransactions, IToolboxService toolboxService)
        {
            var viewModel = new List<PrintJobPureGoldViewModel>();

            foreach (var appObjectToTransaction in appObjectToTransactions)
            {
                // get transaction
                var localTransaction = toolboxService.TransactionService.GetTransactionById(appObjectToTransaction.TransactionId);

                // MRC
                AppObjectToTransaction localAppObjectToTransaction = appObjectToTransaction;
                var taskPrintJobToAppObjectToTransaction =
                    Task.Factory.StartNew(() => toolboxService.PrintJobService
                                                .GetPrintJobAppObjectToTransaction(printJobId, localAppObjectToTransaction.AppObjectToTransactionId));

                //var taskMailing =
                //    taskPrintJobToAppObjectToTransaction.ContinueWith((t) => toolboxService.PrintJobService
                //                                .GetMailing(localAppObjectToTransaction.AppObjectToTransactionId));

                // AtlasXContext
                var taskPropertyTask = Task.Factory.StartNew(() => toolboxService.AtlasXService.GetPropertyById(localTransaction.AtlasXPropertyId));
                var taskPropertyAltTask = taskPropertyTask.ContinueWith((t) => toolboxService.AtlasXService.GetPropertyAlternateById(localTransaction.AtlasXPropertyAlternateId));

                var transactionlist = new List<Transaction> { localTransaction };
                var taskAgentViewModels = Task.Factory.StartNew(() => AgentViewModelBuilder.GetViewModels(transactionlist, toolboxService));

                Task.WaitAll(taskPrintJobToAppObjectToTransaction, taskPropertyAltTask, taskAgentViewModels);

                viewModel.Add(new PrintJobPureGoldViewModel
                {
                    PureGoldMailings = appObjectToTransaction.PureGoldMailings, //taskMailing.Result,
                    AppObjectToTransaction = appObjectToTransaction,
                    PrintJobToAppObjectToTransaction = taskPrintJobToAppObjectToTransaction.Result,
                    Transaction = localTransaction,
                    Property = taskPropertyTask.Result,
                    PropertyAlternate = taskPropertyAltTask.Result,
                    AgentViewModels = taskAgentViewModels.Result
                });
            }

            return viewModel;
        }        
    }
}