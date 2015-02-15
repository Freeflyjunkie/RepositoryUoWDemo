using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels;
using Microsoft.Ajax.Utilities;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class TransactionViewModelBuilder
    {      
        public static TransactionViewModel GetViewModel(int transactionId, IToolboxService toolboxService)
        {
            // Entity Framework is single threaded...

            // MrcContext
            var transaction = toolboxService.TransactionService.GetTransactionById(transactionId);
            var getMailingsTask = Task.Factory.StartNew(() => toolboxService.PrintJobService.GetMailings(transactionId));
            var getChangeRequestDetailParsed =
                getMailingsTask.ContinueWith((t) => ChangeRequestViewModelBuilder.GetChangeRequestDetailsParsed(transaction.ChangeRequests, toolboxService));           
            var getCurrentPrintJob =
                getChangeRequestDetailParsed.ContinueWith((t) => toolboxService.PrintJobService.GetCurrentPrintJob());           
            var getPrintJobToAppObjectToTransactionIds =
                getCurrentPrintJob.ContinueWith((t) => toolboxService.PrintJobService.GetPrintJobToAppObjectToTransactionsByAppObjectToTransactionIds
                        (getCurrentPrintJob.Result == null ? 0 : getCurrentPrintJob.Result.PrintJobId, 
                        getMailingsTask.Result.Select(m => m.AppObjectToTransactionId)));
            
            // AtlasXContext
            var getPropertyTask = Task.Factory.StartNew(() => toolboxService.AtlasXService.GetPropertyById(transaction.AtlasXPropertyId));
            var getPropertyAltTask = getPropertyTask.ContinueWith((t) => toolboxService.AtlasXService.GetPropertyAlternateById(transaction.AtlasXPropertyAlternateId));

            // WeichertSLContext
            var getListTask = Task.Factory.StartNew(() => toolboxService.WeichertSLService.GetListByMrcTransactionId(transactionId));
           
            Task.WaitAll(getPrintJobToAppObjectToTransactionIds, getPropertyAltTask, getListTask);    

            // WeichertCoreContext                     
            var getPersonNumbersTask = Task.Factory.StartNew(() => AgentViewModelBuilder.GetAllAgentPersonNumbers(transaction, getListTask.Result));
            var getPersonsTask = getPersonNumbersTask.ContinueWith((t) => toolboxService.WeichertCoreService.GetPersons(getPersonNumbersTask.Result));
            var getAgentViewModelsTask = getPersonsTask.ContinueWith((t) => AgentViewModelBuilder.GetViewModels(getPersonsTask.Result, toolboxService));

            Task.WaitAll(getAgentViewModelsTask);

            return new TransactionViewModel
            {
                Transaction = transaction,
                Property = getPropertyTask.Result,
                PropertyAlternate = getPropertyAltTask.Result,
                List = getListTask.Result,
                AgentViewModels = getAgentViewModelsTask.Result,
                Mailings = getMailingsTask.Result,
                CurrentPrintJob = getCurrentPrintJob.Result,
                PrintJobToAppObjectToTransactions = getPrintJobToAppObjectToTransactionIds.Result,
                ChangeRequestDetailParsed = getChangeRequestDetailParsed.Result
            };
        }       
    }
}