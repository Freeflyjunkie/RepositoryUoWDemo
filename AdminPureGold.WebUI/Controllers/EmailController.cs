using System;
using System.Threading;
using System.Collections.Generic;
using System.Web.Mvc;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;

namespace AdminPureGold.WebUI.Controllers
{
    public class EmailController : Controller
    {       
        private readonly IToolboxService _toolboxService;
        private readonly List<string> _printTypes;
        private readonly List<string> _months;
        private readonly List<string> _years;

        public EmailController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
            _printTypes = new List<string>() { "Anniversary/Follow up", "Spring Newsletter", "Fall Newsletter" };
            _months = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            _years = new List<string>() { DateTime.Now.Year.ToString(), DateTime.Now.AddYears(1).Year.ToString() };
        }

        [HttpGet]
        public ActionResult Index()
        {

            var checkModel = EmailViewModelBuilder.GetPureGoldEmailSettings(_toolboxService);
            var isMailing = checkModel.PureGoldEmailSettings.SendEmailsFlag;

            if (isMailing == "0")
            {
                ViewBag.IsCurrentlyEmailing = "";

                ViewData["PrintTypes"] = _printTypes;
                ViewData["Months"] = _months;
                ViewData["Years"] = _years;

                ViewBag.PrintType = "Select A Print Type";

                ViewBag.Month1 = DateTime.Now.Month.ToString();
                ViewBag.Year1 = DateTime.Now.Year.ToString();

                ViewBag.Month2 = DateTime.Now.AddMonths(1).Month.ToString();
                ViewBag.Year2 = DateTime.Now.AddMonths(1).Year.ToString();

                ViewBag.IsPostBack = false;

            }
            else
            {
                var viewModel = EmailViewModelBuilder.GetAllMailingsInTable(_toolboxService);

                ViewBag.IsCurrentlyEmailing = "Emails Are Sending";

                ViewData["PrintTypes"] = _printTypes;
                ViewData["Months"] = _months;
                ViewData["Years"] = _years;

                ViewBag.PrintType = "Select A Print Type";

                ViewBag.Month1 = DateTime.Now.Month.ToString();
                ViewBag.Year1 = DateTime.Now.Year.ToString();

                ViewBag.Month2 = DateTime.Now.AddMonths(1).Month.ToString();
                ViewBag.Year2 = DateTime.Now.AddMonths(1).Year.ToString();

                ViewBag.IsPostBack = false;

                return View("Index", viewModel);

            }

            return View();
        }

        [HttpGet]
        public ActionResult LoadData(string printType, Int32 month1, Int32 year1, Int32 month2, Int32 year2)
        {
            ViewBag.IsCurrentlyEmailing = "Data is loaded";

            string passType = printType;
            if (printType == "Anniversary/Follow up")
            {
                passType = "Anniversary";
            }

            _toolboxService.EmailService.LoadPureGoldEmailTable(passType, month1, year1, month2, year2);

            ViewData["PrintTypes"] = _printTypes;
            ViewData["Months"] = _months;
            ViewData["Years"] = _years;

            ViewBag.PrintType = printType;
            ViewBag.Month1 = month1.ToString();
            ViewBag.Year1 = year1.ToString();
            ViewBag.Month2 = month2.ToString();
            ViewBag.Year2 = year2.ToString();

            ViewBag.IsPostBack = true;

            var viewModel = EmailViewModelBuilder.GetAllMailingsInTable(_toolboxService);
            return View("Index", viewModel);

        }


        [HttpPost]
        public JsonResult SaveDueDateAndSetEmailFlag(DateTime dueDate)
        {

            var result = "load";
            try
            {
                _toolboxService.EmailService.SaveDueDateAndSetEmailFlag(dueDate, "1");
                result = "okay";
            }
            catch
            {
                result = "fail";
            }


            return Json(new
            {
                Result = result
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult SendingEmail_StatusNumbers()
        {

            var unsent = "0";
            var sent = "0";

            try
            {
                unsent = _toolboxService.EmailService.GetPureGoldEmails_Pending_Count().ToString();

            }
            catch
            {
                unsent = "fail";
            }
            try
            {
                sent = _toolboxService.EmailService.GetPureGoldEmails_Sent_Count().ToString();

            }
            catch
            {
                sent = "fail";
            }

            string sentResults = sent + ":" + unsent;
            return Json(sentResults, JsonRequestBehavior.AllowGet);

        }
    }
}
