using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels;
using Microsoft.Ajax.Utilities;
using System;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class SurveyViewModelBuilder
    {
        public static SurveyViewModel GetViewModel(IToolboxService toolboxService, int saleId)
        {
            
            // MRC
            var taskSurveyQuestions =  Task.Factory.StartNew(() => toolboxService.SurveyService.GetActiveQuestions());
            var taskSurveyChoices = taskSurveyQuestions.ContinueWith((task) => toolboxService.SurveyService.GetActiveChoices());
            var taskSurvey = taskSurveyChoices.ContinueWith((task) => toolboxService.SurveyService.GetSurveyBySaleId(saleId));
            var taskSurveyAnswers = taskSurvey.ContinueWith((task) => toolboxService.SurveyService.GetSurveyAnswersBySaleId(saleId));
            var taskSurveyAnswerText = taskSurveyAnswers.ContinueWith((task) => toolboxService.SurveyService.GetSurveyAnswersTextBySaleId(saleId));

            // WeichertCore
            //var taskWeichertCore = Task.Factory.StartNew(() => toolboxService.SurveyService.GetActiveQuestions());

            // WeichertSLContext
            var getListTask = Task.Factory.StartNew(() => toolboxService.WeichertSLService.GetListBySaleId(saleId));


            // Enter last task in chain
            // Task.WaitAll(taskSurveyQuestions);//, taskWeichertCore);
            Task.WaitAll(taskSurveyAnswerText, getListTask);//, taskWeichertCore);

            return new SurveyViewModel
            {
                SurveyQuestions = taskSurveyQuestions.Result
                , SurveyChoices = taskSurveyChoices.Result
                , Surveys = taskSurvey.Result
                , SurveyAnswers = taskSurveyAnswers.Result
                , SurveyAnswersTexts = taskSurveyAnswerText.Result
                , List = getListTask.Result
            };

        }

        public static SurveyViewModel GetReportModel(IToolboxService toolboxService, DateTime startDate, DateTime endDate)
        {

            // MRC
            var taskSurveyQuestions = Task.Factory.StartNew(() => toolboxService.SurveyService.GetActiveQuestions());
            var taskSurveyChoices = taskSurveyQuestions.ContinueWith((task) => toolboxService.SurveyService.GetActiveChoices());
            var taskSurvey = taskSurveyChoices.ContinueWith((task) => toolboxService.SurveyService.GetSurveyByDateRange(startDate, endDate));
            var taskSurveyAnswers = taskSurvey.ContinueWith((task) => toolboxService.SurveyService.GetSurveyAnswersByDateRange(startDate, endDate));

            // WeichertCore
            //var taskWeichertCore = Task.Factory.StartNew(() => toolboxService.SurveyService.GetActiveQuestions());

            // Enter last task in chain
            // Task.WaitAll(taskSurveyQuestions);//, taskWeichertCore);
            Task.WaitAll(taskSurveyAnswers);//, taskWeichertCore);

            return new SurveyViewModel
            {
                SurveyQuestions = taskSurveyQuestions.Result,
                SurveyChoices = taskSurveyChoices.Result,
                Surveys = taskSurvey.Result,
                SurveyAnswers = taskSurveyAnswers.Result
            };

        }

        public static SurveyViewModel GetReportDetailModel(IToolboxService toolboxService, int choiceId, DateTime startDate, DateTime endDate)
        {
            var taskSurveyQuestions = Task.Factory.StartNew(() => toolboxService.SurveyService.GetQuestionByChoiceId(choiceId));
            var taskSurveyChoices = taskSurveyQuestions.ContinueWith((task) => toolboxService.SurveyService.GetChoiceByChoiceId(choiceId));
            var taskSurveyReportDetail = taskSurveyChoices.ContinueWith((task) => toolboxService.SurveyService.GetSurveyReportDetail(choiceId, startDate, endDate));

            Task.WaitAll(taskSurveyReportDetail);

            return new SurveyViewModel {
                SurveyQuestions = taskSurveyQuestions.Result,
                SurveyChoices = taskSurveyChoices.Result,
                SurveyReportDetail = taskSurveyReportDetail.Result 
            };
        }


    }
}