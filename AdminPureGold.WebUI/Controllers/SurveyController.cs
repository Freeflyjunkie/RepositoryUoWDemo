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
using System.Web;

namespace AdminPureGold.WebUI.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IToolboxService _toolboxService;

        public SurveyController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }

        [HttpGet]
        public ActionResult Index(int saleId)
        {
            ViewBag.Text = "New";
            ViewBag.SaleId = saleId;

            var survey = _toolboxService.SurveyService.GetSurveyBySaleId(saleId);
            if (survey.Any())
            {
                var viewModel = SurveyViewModelBuilder.GetViewModel(_toolboxService, saleId);

                ViewBag.Address = viewModel.List.ReferenceNumber + " - " + viewModel.List.ListProperty.Address1;
                return View(viewModel);
            }
            else
            {
                _toolboxService.SurveyService.SaveSurvey("", saleId, "Admin On WeichertOne");
                var viewModel = SurveyViewModelBuilder.GetViewModel(_toolboxService, saleId);

                ViewBag.Address = viewModel.List.ReferenceNumber + " - " + viewModel.List.ListProperty.Address1;
                return View(viewModel);
            }

        }

        [HttpGet]
        public ActionResult ViewSurvey(int saleId)
        {
            ViewBag.Text = "New";
            ViewBag.SaleId = saleId;
            ViewBag.Address = "No Property Information Found";

            var viewModel = SurveyViewModelBuilder.GetViewModel(_toolboxService, saleId);

            if (!viewModel.Surveys.Any())
            {
                _toolboxService.SurveyService.SaveSurvey("", saleId, "Admin On WeichertOne");
                viewModel = SurveyViewModelBuilder.GetViewModel(_toolboxService, saleId);
            }

            ViewBag.Address = viewModel.List.ReferenceNumber + " - " + viewModel.List.ListProperty.Address1;
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult SaveSurveyAnswer(int surveyId, int choiceId)
        {
            _toolboxService.SurveyService.SaveSurveyAnswer(surveyId, choiceId);
            return Json("{success: 1}", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveSurveyAnswerText(int surveyId, int choiceId, string surveyAnswerText)
        {
            _toolboxService.SurveyService.SaveSurveyAnswerText(surveyId, choiceId, surveyAnswerText);
            return Json("{success: 1}", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSurveyAnswer(int surveyId, int choiceId)
        {
            _toolboxService.SurveyService.DeleteSurveyAnswer(surveyId, choiceId);
            return Json("{success: 1}", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Report()
        {
            DateTime startDate = DateTime.Now.AddDays(-(DateTime.Now.Day) + 1);
            DateTime endDate = DateTime.Now;

            ViewBag.startDate = startDate.ToShortDateString();
            ViewBag.enddate = endDate.ToShortDateString();

            var reportModel = SurveyViewModelBuilder.GetReportModel(_toolboxService, startDate, endDate);
            return View(reportModel);
        }

        [HttpPost]
        public ActionResult Report(string startDate, string endDate)
        {
            DateTime sDate;
            DateTime eDate;

            DateTime.TryParse(startDate, out sDate);
            DateTime.TryParse(endDate, out eDate);

            ViewBag.startDate = startDate;
            ViewBag.enddate = endDate;

            var reportModel = SurveyViewModelBuilder.GetReportModel(_toolboxService, sDate, eDate);
            return View(reportModel);
        }

        [HttpGet]
        public ActionResult ReportDetail(string choiceId, string startDate, string endDate)
        {
            DateTime sDate;
            DateTime eDate;

            DateTime.TryParse(startDate, out sDate);
            DateTime.TryParse(endDate, out eDate);

            ViewBag.startDate = startDate;
            ViewBag.enddate = endDate;

            int iChoiceId;
            int.TryParse(choiceId, out iChoiceId);

            var reportDetailModel = SurveyViewModelBuilder.GetReportDetailModel(_toolboxService, iChoiceId, sDate, eDate);

            if (reportDetailModel != null)
            {
                var theQuestion = reportDetailModel.SurveyQuestions.Take(1).SingleOrDefault();
                var theChoice = reportDetailModel.SurveyChoices.Take(1).SingleOrDefault();

                ViewBag.Question = theQuestion.QuestionText;
                ViewBag.Choice = theChoice.ChoiceText;

            }

            return View(reportDetailModel);
        }

        [HttpGet]
        public ViewResult Search(String referenceNumber)
        {
            if (!String.IsNullOrEmpty(referenceNumber))
            {
                if (referenceNumber.Length >= 9)
                {
                    var viewModel = SurveySearchViewModelBuilder.GetViewModel(referenceNumber, _toolboxService);
                    return View("Search", viewModel);
                }
            }
            return View("Search");
        }
    }
}
