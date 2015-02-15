using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;
using AdminPureGold.WebUI.ViewModels;
using PagedList;

namespace AdminPureGold.WebUI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IToolboxService _toolboxService;
        public TransactionController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }

        #region Quality Assurance Index
        //public ActionResult QualityAssuranceIndex()
        //{
        //    var qaIssues = _toolboxService.QualityAssuranceService.ListQualityAssuranceIssues();
        //    var viewModel = new QualityAssuranceIndexViewModel
        //    {
        //        QualityAssuranceIssues = qaIssues
        //    };

        //    return View("QualityAssuranceIndex", viewModel);
        //}

        public ActionResult QualityAssuranceIndexByType(int type, int page)
        {
            var viewModel = QualityAssuranceIndexViewModelBuilder.GetViewModelWithoutIssues
                (type, page, _toolboxService);
            return View("QualityAssuranceIndex", viewModel);
        }

        //public PartialViewResult GetFilterPartial(int type, int page)
        //{
        //    var viewModel = QualityAssuranceIndexViewModelBuilder.GetViewModel
        //        (type, page, _toolboxService);

        //    string partialView;
        //    switch ((QualityAssuranceType)type)
        //    {
        //        case QualityAssuranceType.InvalidAddress:
        //            partialView = "_QualityAssuranceIndexInvalidAddressesPartial";
        //            break;

        //        case QualityAssuranceType.NoOwnership:
        //            partialView = "_QualityAssuranceIndexNoOwnershipPartial";
        //            break;

        //        case QualityAssuranceType.MissingData:
        //            partialView = "_QualityAssuranceIndexMissingDataPartial";
        //            break;

        //        case QualityAssuranceType.PrintJob:
        //            partialView = "_QualityAssuranceIndexPrintJobPartial";
        //            break;

        //        default:
        //            partialView = String.Empty;
        //            throw new InvalidEnumArgumentException("type", type, GetType());
        //    }

        //    return PartialView(partialView, viewModel);
        //}
        #endregion

        #region Quality Assurance Edit
        public ActionResult QualityAssuranceEdit(int changeRequestId)
        {
            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var transactionId = changeRequest.TransactionId;
            var viewModel = TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService);

            return View(viewModel);
        }

        public ActionResult QualityAssuranceEditByTransactionId(int transactionId, Int32? currentPrintJobPage, Int32? currentPrintJobItemStatusId)
        {
            if (Session["CurrentPrintJobPage"] == null)
                Session["CurrentPrintJobPage"] = currentPrintJobPage;

            if (Session["CurrentPrintJobItemStatusId"] == null)
                Session["CurrentPrintJobItemStatusId"] = currentPrintJobItemStatusId;


            var viewModel = TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService);

            return View("QualityAssuranceEdit", viewModel);
        }

        public ActionResult BackToCurrentPrintJob(Int32 printJobId)
        {
            var currentPrintJobPage = Session["CurrentPrintJobPage"];
            var currentPrintJobItemStatusId = Session["CurrentPrintJobItemStatusId"];

            Session["CurrentPrintJobPage"] = null;
            Session["CurrentPrintJobItemStatusId"] = null;

            return RedirectToAction("Detail", "PrintJob", new
            {
                printJobId,
                page = currentPrintJobPage,
                printJobItemStatusId = currentPrintJobItemStatusId
            });
        }

        public ActionResult ExcludeFromPrintJob(Int32 appObjectToTransactionId, Int32 printJobId)
        {
            var currentPrintJobPage = Session["CurrentPrintJobPage"];
            var currentPrintJobItemStatusId = Session["CurrentPrintJobItemStatusId"];
            _toolboxService.PrintJobService.ExcludeAppObectToTransaction(printJobId, appObjectToTransactionId);
            return RedirectToAction("BackToCurrentPrintJob", new { printJobId });
            //return RedirectToAction("QualityAssuranceEditByTransactionId", new { transactionId, currentPrintJobPage, currentPrintJobItemStatusId });
        }

        public ActionResult IncludeFromPrintJob(Int32 appObjectToTransactionId, Int32 printJobId)
        {
            var currentPrintJobPage = Session["CurrentPrintJobPage"];
            var currentPrintJobItemStatusId = Session["CurrentPrintJobItemStatusId"];
            _toolboxService.PrintJobService.IncludeAppObectToTransaction(printJobId, appObjectToTransactionId);
            return RedirectToAction("BackToCurrentPrintJob", new { printJobId });
            //return RedirectToAction("QualityAssuranceEditByTransactionId", new { transactionId, currentPrintJobPage, currentPrintJobItemStatusId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTransactionStatus(int transactionId, string activeFlag)
        {
            if (activeFlag == "Activate")
            {
                _toolboxService.TransactionService.ActivateTransaction(transactionId);
            }
            else
            {
                _toolboxService.TransactionService.DeactivateTransaction(transactionId);
            }

            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTransactionData(int transactionId, string customerName, string nameOnEnvelope)
        {
            _toolboxService.TransactionService.UpdateTransactionData(transactionId, Convert.ToInt32(User.Identity.Name), customerName, nameOnEnvelope);
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTransactionProperty(int transactionId, string address1, string address2, string city, string state, string zip)
        {
            Int32 atlasXPropertyId;
            Int32 atlasXPropertyAlternateId;
            _toolboxService.AtlasXService.GetValidatedPropertyIds(Convert.ToInt32(User.Identity.Name),
                address1, address2, city, state, zip, out atlasXPropertyId, out atlasXPropertyAlternateId);
            _toolboxService.TransactionService.UpdateTransactionProperty(transactionId, atlasXPropertyId, atlasXPropertyAlternateId);

            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        public JsonResult SaveTransactionPropertyAjax(int transactionId, string addressLine1, string city, string state, string zip)
        {
            Int32 atlasXPropertyId;
            Int32 atlasXPropertyAlternateId;
            _toolboxService.AtlasXService.GetValidatedPropertyIds(Convert.ToInt32(User.Identity.Name),
                addressLine1, "", city, state, zip, out atlasXPropertyId, out atlasXPropertyAlternateId);
            _toolboxService.TransactionService.UpdateTransactionProperty(transactionId, atlasXPropertyId, atlasXPropertyAlternateId);

            return Json("{success: 1}", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAgentToTransaction(int transactionId, int addedPersonNumber, int addedRelationshipNumber, int addedOfficeId)
        {
            _toolboxService.TransactionService.UpdateTransactionAgent(transactionId, addedPersonNumber, addedOfficeId, addedRelationshipNumber);
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAgentStatus(int transactionId, int relationshipNumber, string status)
        {
            _toolboxService.TransactionService.UpdateTransactionAgentStatus(transactionId, relationshipNumber, status);
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAgentPrimary(int transactionId, int relationshipNumber)
        {
            _toolboxService.TransactionService.UpdateTransactionAgentToPrimary(transactionId, relationshipNumber);
            return View("QualityAssuranceEdit", TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAgentSecondary(int transactionId, int relationshipNumber)
        {
            _toolboxService.TransactionService.UpdateTransactionAgentToSecondary(transactionId, relationshipNumber);
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        public ActionResult AddToPrintJob(int mailingId, string printType, DateTime scheduledPrintDate, int appObjectToTransactionId)
        {
            var printJobType = _toolboxService.PrintJobService.GetPrintJobTypeEnum(printType);
            var printjob = _toolboxService.PrintJobService.GetCurrentPrintJob() ??
                           _toolboxService.PrintJobService.CreateEmptyPrintJob(printJobType, printType, scheduledPrintDate.Date,
                               scheduledPrintDate.Date, Convert.ToInt32(User.Identity.Name));

            // make sure the printjob type can add the mailing
            // print jobs can only contain one type
            if ((Int32)printJobType == printjob.PrintJobTypeId)
            {
                _toolboxService.PrintJobService.UpdatePrintJobAppObjectToTransactions(printjob.PrintJobId, appObjectToTransactionId);
            }

            return RedirectToAction("DetailPureGold", "PrintJob", new { printJobId = printjob.PrintJobId });
        }

        public ActionResult MailingSetPrintStatus(int mailingId, int appObjectToTransactionId)
        {
            _toolboxService.PrintJobService.UpdateMailing(mailingId);
            var transactionId =
                _toolboxService.TransactionService.GetTransactionIdByAppObjectToTransactionId(appObjectToTransactionId);

            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        public JsonResult GetAgentsByName(string search)
        {
            var nameSearch = _toolboxService.WeichertCoreService.GetAgentByLastNameOrAssociateNumber(search);
            var searchResults = nameSearch.Select(name => new
            {
                FirstName = name.LicenseFname.Trim() + " (" + name.Office.Trim() + ")",
                LastName = name.LicenseLname.Trim(),
                RelationshipNumber = name.Wrelateno,
                PersonNumber = name.Wpersno,
                OfficeId = name.OfficeId
            }).ToList();

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendCommentsToAgent(int transactionId, string comments)
        {
            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CloseChangeRequestWithComments(int changeRequestId, string comments)
        {
            _toolboxService.ChangeRequestService.ChangeRequestClose(changeRequestId,
                Convert.ToInt32(User.Identity.Name),
                comments);

            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var transactionId = changeRequest.TransactionId;

            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptChangeRequest(int changeRequestId, int changeRequestCategory)
        {
            var parsedDetail = ChangeRequestViewModelBuilder.GetChangeRequestDetailParsed(changeRequestId, _toolboxService);

            _toolboxService.ChangeRequestService.ChangeRequestCloseAndApply(changeRequestId,
               Convert.ToInt32(User.Identity.Name),
               parsedDetail);

            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var transactionId = changeRequest.TransactionId;
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DenyChangeRequestWithComments(int changeRequestId, string comments)
        {
            _toolboxService.ChangeRequestService.ChangeRequestDeny(changeRequestId,
                Convert.ToInt32(User.Identity.Name),
                comments);

            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var transactionId = changeRequest.TransactionId;
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveChangeRequestComment(int changeRequestId, string comments)
        {
            _toolboxService.ChangeRequestService.UpdateChangeRequestsComment(changeRequestId, Convert.ToInt32(User.Identity.Name), comments);

            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var transactionId = changeRequest.TransactionId;
            return View("QualityAssuranceEdit", TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReOpenChangeRequest(int changeRequestId)
        {
            _toolboxService.ChangeRequestService.UpdateChangeRequestsStatus(changeRequestId, 600);

            var changeRequest = _toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var transactionId = changeRequest.TransactionId;
            return View("QualityAssuranceEdit",
                TransactionViewModelBuilder.GetViewModel(transactionId, _toolboxService));
        }

        [HttpPost]
        public JsonResult FindLocationByAddress(string addressLine1, string city, string state, string zip)
        {
            var location = _toolboxService.VirtualEarthService.FindLocationByAddress(addressLine1, city, state, zip);
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ValidatePropertyUsingUsps(string addressLine1, string city, string state, string zip)
        {
            var uspsValidateProperty = _toolboxService.AtlasXService.ValidatePropertyUsingUsps(addressLine1, "", city, state, zip);
            return Json(uspsValidateProperty, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult Search()
        {
            var viewModel = new List<SearchViewModel>();

            ViewBag.HasPreviousPage = false;
            ViewBag.HasNextPage = false;
            ViewBag.IsPostBack = false;

            var pviewModel = viewModel.ToPagedList(1, 1);
            return View("Search", pviewModel);
        }

        public ViewResult SearchBy(String search, String searchType, Int32? page)
        {
            const int pageSize = 10;
            int pageNumber = (page ?? 1);
            var transactions = _toolboxService.TransactionService.Search(search, searchType).ToList();            

            ViewBag.IsPostBack = true;

            if (transactions.Any())
            {
                var pagedTransactions = transactions.ToPagedList(pageNumber, pageSize);
                var viewModel = SearchViewModelBuilder.GetViewModels(pagedTransactions, _toolboxService);
                ViewBag.Total = transactions.Count();
                ViewBag.SearchType = searchType;
                ViewBag.Search = search;
                ViewBag.PageCount = pagedTransactions.PageCount;
                ViewBag.PageNumber = pagedTransactions.PageNumber;
                ViewBag.HasPreviousPage = pagedTransactions.HasPreviousPage;
                ViewBag.HasNextPage = pagedTransactions.HasNextPage;
                
                return View("Search", viewModel);
            }
            else
            {
                var viewModel = new List<SearchViewModel>();

                ViewBag.HasPreviousPage = false;
                ViewBag.HasNextPage = false;
                ViewBag.IsPostBack = false;

                var pviewModel = viewModel.ToPagedList(1, 1);
                return View("Search", pviewModel);

            }
        }

        public ActionResult GetListWithClosedSaleByReferenceNumber(String referenceNumber)
        {
            var viewModel = CreateCustomerViewModelBuilder.GetViewModel(referenceNumber, _toolboxService);
            return View("CreateCustomer", viewModel);
        }

        public ActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCustomer(Int32 listId)
        {
            var newTransactionId = _toolboxService.TransactionService.CreateTransactionFromListId(listId);
            if (newTransactionId == 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("QualityAssuranceEditByTransactionId", new { transactionId = newTransactionId });
            }
        }
    }
}
