using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class SearchViewModelBuilder
    {
        public static IEnumerable<SearchViewModel> GetViewModels
            (IEnumerable<Transaction> transactions, IToolboxService toolboxService)            
        {
            var searchViewModels = new List<SearchViewModel>();

            foreach (var transaction in transactions)
            {
                Transaction localTransaction = transaction;

                // AtlasXContext
                var getPropertyTask = Task.Factory.StartNew(() => toolboxService.AtlasXService.GetPropertyById(localTransaction.AtlasXPropertyId));
                var getPropertyAltTask = getPropertyTask.ContinueWith((t) => toolboxService.AtlasXService.GetPropertyAlternateById(localTransaction.AtlasXPropertyAlternateId));

                var transactionlist = new List<Transaction> { transaction };
                var taskAgentViewModels = Task.Factory.StartNew(() => AgentViewModelBuilder.GetViewModels(transactionlist, toolboxService));

                var saleId = Task.Factory.StartNew(() => toolboxService.WeichertSLService.GetSaleIdByMrcTransactionId(localTransaction.TransactionId));

                Task.WaitAll(getPropertyAltTask, taskAgentViewModels);

                searchViewModels.Add(new SearchViewModel
                {
                    Transaction = transaction,
                    SaleId = saleId.Result,
                    Property = getPropertyTask.Result,
                    PropertyAlternate = getPropertyAltTask.Result,
                    AgentViewModels = taskAgentViewModels.Result
                });
            }
            return searchViewModels;
        }
    }
}