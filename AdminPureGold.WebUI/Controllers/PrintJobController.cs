using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;
using AdminPureGold.WebUI.ViewModels;
using PagedList;

namespace AdminPureGold.WebUI.Controllers
{
    public class PrintJobController : Controller
    {
        private readonly IToolboxService _toolboxService;
        public PrintJobController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }

        public ActionResult Configure()
        {
            ViewBag.PrintJobTypes = _toolboxService.PrintJobService.GetPrintJobTypes();
            var viewModel = _toolboxService.PrintJobService.GetCurrentPrintJob();
            if (viewModel == null)
            {
                return View();
            }

            return RedirectToAction("Detail", new { viewModel.PrintJobId });
        }
        public ActionResult Actuals()
        {
            ViewBag.PrintJobTypes = _toolboxService.PrintJobService.GetPrintJobTypes();
            return View();
        }
        public ActionResult Projections()
        {
            ViewBag.PrintJobTypes = _toolboxService.PrintJobService.GetPrintJobTypes().Where(t => t.PrintJobTypeId != 15);
            return View();
        }
        public ActionResult Detail(Int32 printJobId, Int32? page, Int32? printJobItemStatusId = null)
        {
            var printJob = _toolboxService.PrintJobService.GetPrintJobById(printJobId);
            var action = "DetailPureGold";

            if (printJob.PrintJobTypeId == 15)
            {
                action = "DetailNonPureGold";
            }

            return RedirectToAction(action, new { printJobId, page, printJobItemStatusId });
        }
        public ActionResult DetailNonPureGold(Int32 printJobId, Int32? page, Int32? printJobItemStatusId = null)
        {
            const int pageSize = 8;
            var pageNumber = (page ?? 1);

            // Get PrintJobToWeichertSLIds
            var printJobToWeichertSLIds =
                _toolboxService.PrintJobService.GetPrintJobToWeichertSLIds(printJobId, printJobItemStatusId).ToList();

            // Page 
            var pagedPrintJobToWeichertSLIds = printJobToWeichertSLIds.ToPagedList(pageNumber, pageSize);

            // Populate View Model
            var viewModel = PrintJobPureGoldViewModelBuilder.GetViewModels(
                pagedPrintJobToWeichertSLIds, _toolboxService);

            ViewBag.PrintJobItemStatuses = _toolboxService.PrintJobService.GetPrintJobItemStatuses();
            ViewBag.PrintJobItemStatusSelected = printJobItemStatusId;
            ViewBag.PrintJob = _toolboxService.PrintJobService.GetPrintJobById(printJobId);
            ViewBag.PageCount = pagedPrintJobToWeichertSLIds.PageCount;
            ViewBag.PageNumber = pagedPrintJobToWeichertSLIds.PageNumber;
            ViewBag.HasPreviousPage = pagedPrintJobToWeichertSLIds.HasPreviousPage;
            ViewBag.HasNextPage = pagedPrintJobToWeichertSLIds.HasNextPage;
            ViewBag.Total = printJobToWeichertSLIds.Count();

            return View(viewModel);
        }
        public ActionResult DetailPureGold(Int32 printJobId, Int32? page, Int32? printJobItemStatusId = null)
        {
            const int pageSize = 8;
            var pageNumber = (page ?? 1);

            // Get PrintJobToWeichertSLIds
            var printJobToAppObjectToTransactionIds =
                _toolboxService.PrintJobService.GetPrintJobAppObjectToTransactionIds(printJobId, printJobItemStatusId).ToList();

            // Page
            var pagedPrintJobAppObjectToTransactionIds =
                printJobToAppObjectToTransactionIds.ToPagedList(pageNumber, pageSize);

            // Populate View Model
            var viewModel = PrintJobPureGoldViewModelBuilder.GetViewModels(printJobId,
                pagedPrintJobAppObjectToTransactionIds, _toolboxService);

            ViewBag.PrintJobItemStatuses = _toolboxService.PrintJobService.GetPrintJobItemStatuses();
            ViewBag.PrintJobItemStatusSelected = printJobItemStatusId;
            ViewBag.PrintJob = _toolboxService.PrintJobService.GetPrintJobById(printJobId);
            ViewBag.PageCount = pagedPrintJobAppObjectToTransactionIds.PageCount;
            ViewBag.PageNumber = pagedPrintJobAppObjectToTransactionIds.PageNumber;
            ViewBag.HasPreviousPage = pagedPrintJobAppObjectToTransactionIds.HasPreviousPage;
            ViewBag.HasNextPage = pagedPrintJobAppObjectToTransactionIds.HasNextPage;
            ViewBag.Total = printJobToAppObjectToTransactionIds.Count();

            return View(viewModel);
        }
        public ActionResult Missed(Int32 printJobId, Int32? page)
        {
            const int pageSize = 8;
            var pageNumber = (page ?? 1);

            // Get AppObjectToTransactions
            var appObjectToTransactions = _toolboxService.PrintJobService.GetMissingCustomers(printJobId);

            // Page 
            var pagedAppObjectToTransactions = appObjectToTransactions.ToPagedList(pageNumber, pageSize);

            // Build Paged Model
            var viewModel = PrintJobPureGoldViewModelBuilder.GetViewModels(printJobId,
                pagedAppObjectToTransactions, _toolboxService);

            ViewBag.PrintJobItemStatuses = _toolboxService.PrintJobService.GetPrintJobItemStatuses();
            ViewBag.PrintJob = _toolboxService.PrintJobService.GetPrintJobById(printJobId);
            ViewBag.PageCount = pagedAppObjectToTransactions.PageCount;
            ViewBag.PageNumber = pagedAppObjectToTransactions.PageNumber;
            ViewBag.HasPreviousPage = pagedAppObjectToTransactions.HasPreviousPage;
            ViewBag.HasNextPage = pagedAppObjectToTransactions.HasNextPage;
            ViewBag.Total = pagedAppObjectToTransactions.Count();

            return View(viewModel);
        }
        public ActionResult History(Int32? page)
        {
            const int pageSize = 8;
            var pageNumber = (page ?? 1);

            // Get PrintJob Ids
            var printJobIds = _toolboxService.PrintJobService.GetPrintJobIds().ToList();

            //Page 
            var pagedPrintJobIds = printJobIds.ToPagedList(pageNumber, pageSize);

            // Build Model 
            var viewModel = _toolboxService.PrintJobService.GetPrintJobsByPrintJobIds(pagedPrintJobIds);

            ViewBag.PageCount = pagedPrintJobIds.PageCount;
            ViewBag.PageNumber = pagedPrintJobIds.PageNumber;
            ViewBag.HasPreviousPage = pagedPrintJobIds.HasPreviousPage;
            ViewBag.HasNextPage = pagedPrintJobIds.HasNextPage;
            ViewBag.Total = printJobIds.Count();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetActuals(Int32 type, String mailingTypeText, DateTime startdate, DateTime enddate)
        {
            var count = _toolboxService.PrintJobService.GetPrintJobActuals((PrintJobTypeEnum)type, startdate, enddate);

            ViewBag.Count = count;
            ViewBag.MailingType = mailingTypeText; ViewBag.PrintJobTypes = _toolboxService.PrintJobService.GetPrintJobTypes().Where(t => t.PrintJobTypeId != 15);
            ViewBag.PrintJobTypes = _toolboxService.PrintJobService.GetPrintJobTypes();

            return View("Actuals");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetProjections(Int32 type, String mailingTypeText, DateTime startdate, DateTime enddate)
        {
            var count = _toolboxService.PrintJobService.GetPrintJobProjections((PrintJobTypeEnum)type, startdate, enddate);

            ViewBag.Count = count;
            ViewBag.MailingType = mailingTypeText;
            ViewBag.PrintJobTypes = _toolboxService.PrintJobService.GetPrintJobTypes().Where(t => t.PrintJobTypeId != 15);

            return View("Projections");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(Int32 printJobId)
        {
            _toolboxService.PrintJobService.ExportPrintJob(printJobId);
            var excelExport = _toolboxService.PrintJobService.GetExportedDataForExcel();
            var wordExport = _toolboxService.PrintJobService.GetExportedDataForWord();
            
            ViewBag.PrintJobId = printJobId;

            ViewBag.WordCount = wordExport.Count();
            ViewBag.ExcelCount = excelExport.Count();

            return View(excelExport);
        }

        [HttpPost]
        public ActionResult Update(Int32 printJobId, Int32? page, String printJobDesc)
        {
            _toolboxService.PrintJobService.UpdatePrintJobDescription(printJobId, printJobDesc);

            return RedirectToAction("Detail", new
            {
                printJobId,
                page
            });
        }

        [HttpPost]
        public JsonResult Create(PrintJobTypeEnum type, String description, DateTime startDate, DateTime endDate)
        {
            var printJob = _toolboxService.PrintJobService.GetCurrentPrintJob() ??
                _toolboxService.PrintJobService.CreatePrintJob(type, description, startDate, endDate, Convert.ToInt32(User.Identity.Name));

            var failed = _toolboxService.QualityAssuranceService
                             .ListTransactionsByQualityAssuranceIssueType(QualityAssuranceType.PrintJob)
                             .ToList().Count();

            var total = printJob.PrintJobTypeId == 15
                ? _toolboxService.PrintJobService.GetPrintJobToWeichertSLCount(printJob.PrintJobId, null)
                : _toolboxService.PrintJobService.GetPrintJobToAppObjectToTransactionsCount(printJob.PrintJobId, null);

            var passed = total - failed;

            return Json(new
            {
                PrintJob = printJob,
                Passed = passed,
                Failed = failed,
                Total = total
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcludeAppObjectToTransaction(Int32 printJobId, Int32? page, Int32 appObjectToTransactionId, Int32? printJobItemStatusId = null)
        {
            _toolboxService.PrintJobService.ExcludeAppObectToTransaction(printJobId, appObjectToTransactionId);

            return RedirectToAction("Detail", new
            {
                printJobId,
                page,
                printJobItemStatusId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExcludeSale(Int32 printJobId, Int32? page, Int32 saleId, Int32? printJobItemStatusId = null)
        {
            _toolboxService.PrintJobService.ExcludePrintJobToWeichertSL(printJobId, saleId);

            return RedirectToAction("Detail", new
            {
                printJobId,
                page,
                printJobItemStatusId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncludeAppObjectToTransaction(Int32 printJobId, Int32? page, Int32 appObjectToTransactionId, Int32? printJobItemStatusId = null)
        {
            _toolboxService.PrintJobService.IncludeAppObectToTransaction(printJobId, appObjectToTransactionId);

            return RedirectToAction("Detail", new
            {
                printJobId,
                page,
                printJobItemStatusId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncludeSale(Int32 printJobId, Int32? page, Int32 saleId, Int32? printJobItemStatusId = null)
        {
            _toolboxService.PrintJobService.IncludePrintJobToWeichertSL(printJobId, saleId);

            return RedirectToAction("Detail", new
            {
                printJobId,
                page,
                printJobItemStatusId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarkAsSuccessful(Int32 printJobId)
        {
            _toolboxService.PrintJobService
                .UpdatePrintJobStatus(printJobId, Convert.ToInt32(User.Identity.Name), PrintJobStatusEnum.MarkedAsSuccessful);

            return RedirectToAction("Configure");        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncludeMissed(Int32 printJobId, Int32? page, Int32 appObjectToTransactionId)
        {
            _toolboxService.PrintJobService.IncludeAppObectToTransaction(printJobId, appObjectToTransactionId);

            return RedirectToAction("Missed", new
            {
                printJobId,
                page
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(Int32 printJobId)
        {
            _toolboxService.PrintJobService.UpdatePrintJobStatus(printJobId, Convert.ToInt32(User.Identity.Name), PrintJobStatusEnum.Cancelled);
            return RedirectToAction("Configure");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(Int32 printJobId)
        {
            _toolboxService.PrintJobService.UpdatePrintJobStatus(printJobId, Convert.ToInt32(User.Identity.Name), PrintJobStatusEnum.Unexported);
            return RedirectToAction("Configure");
        }

        public ActionResult Surveys()
        {
            return View();
        }
    }
}
