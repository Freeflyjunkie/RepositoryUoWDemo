using System.Collections.Generic;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.ViewModels;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class ChangeRequestCommentHistoryBuilder
    {
        public static IEnumerable<ChangeRequestCommentHistory> GetViewModels(int changeRequestId, IToolboxService toolboxService)
        {
            var viewModel = new List<ChangeRequestCommentHistory>();
            foreach (var changeRequestComment in toolboxService.ChangeRequestService.GetChangeRequestComments(changeRequestId))
            {
                var person = toolboxService.WeichertCoreService.GetPersonByPersonNumber(changeRequestComment.PersonNumber);
                var agentViewModel = AgentViewModelBuilder.GetViewModel(person, toolboxService);
                viewModel.Add(new ChangeRequestCommentHistory
                {
                    ChangeRequestComment = changeRequestComment,
                    AgentViewModel = agentViewModel
                });
            }
            return viewModel;
        }
    }
}