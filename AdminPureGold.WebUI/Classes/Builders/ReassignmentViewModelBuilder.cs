using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class ReassignmentViewModelBuilder
    {
        public static IEnumerable<ReassignmentViewModel> GetViewModels
            (IEnumerable<Transaction> transactions, IToolboxService toolboxService)
        {
            var viewModels = new List<ReassignmentViewModel>();

            foreach (var transaction in transactions)
            {
                Transaction localTransaction = transaction;

                // AtlasXContext
                var getPropertyTask = Task.Factory.StartNew(() => toolboxService.AtlasXService.GetPropertyById(localTransaction.AtlasXPropertyId));
                var getPropertyAltTask = getPropertyTask.ContinueWith((t) => toolboxService.AtlasXService.GetPropertyAlternateById(localTransaction.AtlasXPropertyAlternateId));

                var transactionlist = new List<Transaction> { transaction };
                var taskAgentViewModels = Task.Factory.StartNew(() => AgentViewModelBuilder.GetViewModels(transactionlist, toolboxService));

                Task.WaitAll(getPropertyTask, getPropertyAltTask, taskAgentViewModels);                

                viewModels.Add(new ReassignmentViewModel
                {
                    Transaction = transaction,
                    Property = getPropertyTask.Result,
                    PropertyAlternate = getPropertyAltTask.Result,
                    AgentViewModels = taskAgentViewModels.Result
                });
            }
            return viewModels;
        }
    }
}