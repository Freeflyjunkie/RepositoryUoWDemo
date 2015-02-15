using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;
using System;

namespace AdminPureGold.WebUI.ViewModels
{
    public class SurveyViewModel
    {
        public IEnumerable<SurveyQuestion> SurveyQuestions { get; set; }

        public IEnumerable<SurveyChoice> SurveyChoices { get; set; }

        public IEnumerable<Survey> Surveys { get; set; }

        public IEnumerable<SurveyAnswer> SurveyAnswers { get; set; }

        public IEnumerable<SurveyAnswerText> SurveyAnswersTexts { get; set; }

        public IEnumerable<SurveyReport> SurveyReportDetail { get; set; }

        public List List { get; set; }

    }
}