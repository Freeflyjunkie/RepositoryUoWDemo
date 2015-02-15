using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class SurveyRepository : GenericRepository<Survey>, ISurveyRepository
    {
        private readonly MrcContext _context;

        public SurveyRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }
        public IEnumerable<Survey> GetSurveysBySaleId(Int32 saleId)
        {
            return _context.Surveys
                .Where(a => a.SaleId  == saleId)
                .ToList();
        }
        public IEnumerable<Survey> GetSurveysBySurveyId(Int32 surveyId)
        {
            return _context.Surveys
                .Where(a => a.SurveyId == surveyId)
                .ToList();
        }
        public IEnumerable<SurveyAnswer> GetSurveyAnswers(Int32 surveyId)
        {
            return _context.SurveyAnswers
                .Where(a => a.SurveyId == surveyId)
                .ToList();
        }
        public IEnumerable<SurveyAnswer> GetSurveyAnswersBySaleId(Int32 saleId)
        {
            var survey = _context.Surveys
                .Where(a => a.SaleId == saleId).ToList().SingleOrDefault();
            
            if (survey != null)
            {                
                return _context.SurveyAnswers
                            .Where(sa => sa.SurveyId == survey.SurveyId).ToList();
            }
            return null;
        }

        public IEnumerable<SurveyAnswerText> GetSurveyAnswersTextBySaleId(Int32 saleId)
        {
            var survey = _context.Surveys.Where(a => a.SaleId == saleId).ToList().SingleOrDefault();
            if (survey != null)
            {
                return _context.SurveyAnswers
                    .Where(a => a.SurveyId  == survey.SurveyId)
                    .Join(_context.SurveyAnswerTexts,
                        a => a.SurveyAnswerId,
                        b => b.SurveyAnswerId,
                        (a, b) => b)
                    .ToList();
            }
            return null;
        }

        public IEnumerable<Survey> GetSurveysByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Surveys
                .Where(a => a.InputDate >= startDate && a.InputDate <= endDate)
                .ToList();
        }
        public IEnumerable<SurveyAnswer> GetSurveyAnswersByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Surveys
                .Where(a => a.InputDate >= startDate && a.InputDate <= endDate)
                .Join(_context.SurveyAnswers,
                    surveys => surveys.SurveyId,
                    surveyanswers => surveyanswers.SurveyId,
                    (surveys, surveyanswers) => surveyanswers)
                .ToList();
        }
    }
}
