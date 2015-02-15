using System.Linq;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Classes;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.ViewModels;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class HomeViewModelBuilder
    {
        public static HomeViewModel GetViewModel(IToolboxService toolboxService)
        {
            var changeRequests = toolboxService.ChangeRequestService.GetChangeRequestsByStatus("Open").ToList();

            // TPL
            var taskGetChangeRequestDetailParsed =
                Task.Factory.StartNew(() => ChangeRequestViewModelBuilder.GetChangeRequestDetailsParsed(changeRequests, toolboxService));

            var taskGetCurrentPrintJob =
                taskGetChangeRequestDetailParsed.ContinueWith((t) => toolboxService.PrintJobService.GetCurrentPrintJob());

            var taskGetQaIssues =
                taskGetCurrentPrintJob.ContinueWith((t) => toolboxService.QualityAssuranceService.ListQualityAssuranceIssues());

            Task.WaitAll(taskGetChangeRequestDetailParsed, taskGetCurrentPrintJob, taskGetQaIssues);

            return new HomeViewModel
            {
                QualityAssuranceIssues = taskGetQaIssues.Result,
                ChangeRequests = changeRequests,
                ChangeRequestDetailParsed = taskGetChangeRequestDetailParsed.Result,
                CurrentPrintJob = taskGetCurrentPrintJob.Result
            };
        }
    }
}