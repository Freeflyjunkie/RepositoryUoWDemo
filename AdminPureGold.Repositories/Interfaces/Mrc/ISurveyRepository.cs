using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface ISurveyRepository : IGenericRepository<Survey>
    {
        IEnumerable<Survey> GetSurveysBySaleId(Int32 saleId);
        IEnumerable<Survey> GetSurveysBySurveyId(Int32 surveyId);
        IEnumerable<SurveyAnswer> GetSurveyAnswers(Int32 surveyId);
        IEnumerable<SurveyAnswer> GetSurveyAnswersBySaleId(Int32 saleId);
        IEnumerable<SurveyAnswerText> GetSurveyAnswersTextBySaleId(Int32 saleId);

        IEnumerable<Survey> GetSurveysByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<SurveyAnswer> GetSurveyAnswersByDateRange(DateTime startDate, DateTime endDate);
    
    }
}
