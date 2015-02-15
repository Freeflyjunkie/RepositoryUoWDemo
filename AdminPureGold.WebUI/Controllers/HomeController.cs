using System;
using System.Web.Mvc;
using AdminPureGold.ApplicationServices.Classes;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;

namespace AdminPureGold.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToolboxService _toolboxService;
        public HomeController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            // Build View Model
            var viewModel = HomeViewModelBuilder.GetViewModel(_toolboxService);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseChangeRequestWithComments(int changeRequestId, string comments)
        {
            // Update Comments And Close
            _toolboxService.ChangeRequestService.ChangeRequestClose
                (changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var viewModel = HomeViewModelBuilder.GetViewModel(_toolboxService);
            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptChangeRequest(int changeRequestId, int changeRequestCategory)
        {
            // Get Parsed Detail
            var parsedDetail = ChangeRequestViewModelBuilder.GetChangeRequestDetailParsed
                (changeRequestId, _toolboxService);

            // Update Comments, Close And Apply
            _toolboxService.ChangeRequestService.ChangeRequestCloseAndApply
                (changeRequestId, Convert.ToInt32(User.Identity.Name), parsedDetail);

            var viewModel = HomeViewModelBuilder.GetViewModel(_toolboxService);
            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DenyChangeRequestWithComments(int changeRequestId, string comments)
        {
            // Update Comments, and Deny
            _toolboxService.ChangeRequestService.ChangeRequestDeny
                (changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var viewModel = HomeViewModelBuilder.GetViewModel(_toolboxService);
            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChangeRequestComment(int changeRequestId, string comments)
        {
            // Update Comments 
            _toolboxService.ChangeRequestService.UpdateChangeRequestsComment
                (changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var viewModel = HomeViewModelBuilder.GetViewModel(_toolboxService);
            return View("Index", viewModel);
        }
    }
}
