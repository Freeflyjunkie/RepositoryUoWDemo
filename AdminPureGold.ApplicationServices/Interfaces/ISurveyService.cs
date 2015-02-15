using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.Domain.Models.Mrc;
using System.Linq;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface ISurveyService
    {
        IEnumerable<SurveyQuestion> GetActiveQuestions();
        IEnumerable<SurveyQuestion> GetInActiveQuestions();
        IEnumerable<SurveyChoice> GetActiveChoices();
        IEnumerable<SurveyChoice> GetInActiveChoices();
        SurveyChoiceType GetChoiceTypeById(int choiceTypeId);
        IEnumerable<Survey> GetSurveyBySaleId(int saleId);
        IEnumerable<SurveyAnswer> GetSurveyAnswersBySurveyId(int surveyId);
        IEnumerable<SurveyAnswer> GetSurveyAnswersBySaleId(int saleId);
        IEnumerable<SurveyAnswerText> GetSurveyAnswersTextBySaleId(int saleId);

        IEnumerable<SurveyQuestion> GetQuestionByChoiceId(Int32 choiceId);
        IEnumerable<SurveyChoice> GetChoiceByChoiceId(Int32 choiceId);


        IEnumerable<Survey> GetSurveyByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<SurveyAnswer> GetSurveyAnswersByDateRange(DateTime startDate, DateTime endDate);

        IEnumerable<SurveyReport> GetSurveyReportDetail(int choiceId, DateTime startDate, DateTime endDate);

        void SaveSurvey(string referenceNumber, int saleId, string inputBy);
        void SaveSurveyAnswer(int surveyId, int choiceId);
        void SaveSurveyAnswerText(int surveyId, int choiceId, string surveyAnswerText);
        void DeleteSurveyAnswer(int surveyId, int choiceId);


    }
}
