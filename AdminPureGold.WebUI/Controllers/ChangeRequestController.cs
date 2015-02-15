using System;
using System.Linq;
using System.Web.Mvc;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;

namespace AdminPureGold.WebUI.Controllers
{
    public class ChangeRequestController : Controller
    {
        private readonly IToolboxService _toolboxService;
        public ChangeRequestController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }

        public ActionResult Index()
        {
            var viewModel = ChangeRequestViewModelBuilder.GetViewModelsByStatus("Open", _toolboxService);
            GetMenuData();

            return View(viewModel);
        }

        public ActionResult MostRecent(int take)
        {
            var viewModel = ChangeRequestViewModelBuilder.GetViewModelByMostRecent(take, _toolboxService);
            GetMenuData();
            SetActionAndTake("MostRecent", take);

            return View("Index", viewModel);
        }

        public ActionResult Filter(Int32 pageNumber, string statusDescription = "", string categoryDescription = "")
        {
            var viewModel = ChangeRequestViewModelBuilder.GetViewModelsByStatusAndCategory
                (statusDescription, categoryDescription, pageNumber, _toolboxService);
            GetMenuData();
            SetActionStatusAndCategory("Filter", statusDescription, categoryDescription);            

            return View("Index", viewModel);
        }        

        [HttpPost]
        public JsonResult GetChangeRequestComments(int changeRequestId)
        {
            return Json(ChangeRequestCommentHistoryBuilder.GetViewModels
                (changeRequestId, _toolboxService), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDetails(int changeRequestId)
        {
            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            return Json(changeRequest.Detail, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseChangeRequestWithComments(int changeRequestId, string comments)
        {
            _toolboxService.ChangeRequestService.ChangeRequestClose
                (changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var viewModel = ChangeRequestViewModelBuilder.GetViewModel
                (changeRequestId, _toolboxService);

            GetMenuData();

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptChangeRequest(int changeRequestId, int changeRequestCategory)
        {
            var parsedDetail = ChangeRequestViewModelBuilder.GetChangeRequestDetailParsed
                (changeRequestId, _toolboxService);

            _toolboxService.ChangeRequestService.ChangeRequestCloseAndApply
                (changeRequestId, Convert.ToInt32(User.Identity.Name), parsedDetail);

            var viewModel = ChangeRequestViewModelBuilder.GetViewModel
                (changeRequestId, _toolboxService);

            GetMenuData();

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DenyChangeRequestWithComments(int changeRequestId, string comments)
        {
            _toolboxService.ChangeRequestService.ChangeRequestDeny
                (changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var viewModel = ChangeRequestViewModelBuilder.GetViewModel(changeRequestId, _toolboxService);

            GetMenuData();

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChangeRequestComment(int changeRequestId, string comments)
        {
            _toolboxService.ChangeRequestService.UpdateChangeRequestsComment
                (changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var viewModel = ChangeRequestViewModelBuilder.GetViewModel
                (changeRequestId, _toolboxService);

            GetMenuData();

            return View("Index", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReOpenChangeRequest(int changeRequestId)
        {
            _toolboxService.ChangeRequestService.UpdateChangeRequestsStatus
                (changeRequestId, 600);

            var viewModel = ChangeRequestViewModelBuilder.GetViewModel
                (changeRequestId, _toolboxService);

            GetMenuData();

            return View("Index", viewModel);
        }

        private void GetMenuData()
        {
            ViewData["Categories"] = _toolboxService.ChangeRequestService.GetChangeRequestCategories().ToList();
            ViewData["Statuses"] = _toolboxService.ChangeRequestService.GetChangeRequestStatuses().ToList();
        }

        private void SetActionAndTake(string action, int take)
        {
            ViewBag.Action = action;
            ViewBag.Take = take;      
        }

        private void SetActionStatusAndCategory(string action, string statusDescription, string categoryDescription)
        {
            ViewBag.Action = action;
            ViewBag.Status = statusDescription;
            ViewBag.Category = categoryDescription;
        }
    }
}
