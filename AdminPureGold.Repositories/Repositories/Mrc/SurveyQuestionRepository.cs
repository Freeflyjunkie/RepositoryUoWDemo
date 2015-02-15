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
    public class SurveyQuestionRepository : GenericRepository<SurveyQuestion>, ISurveyQuestionRepository
    {
        private readonly MrcContext _context;
        public SurveyQuestionRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<SurveyQuestion> GetQuestions(Boolean active)
        {
            return _context.SurveyQuestions
                .Where(a => a.Active == active)                
                .ToList();
        }

        public IEnumerable<SurveyChoice> GetChoices(Boolean active)
        {
            return _context.SurveyChoices
                .Where(a => a.Active == active)
                .ToList();
        }

        public IEnumerable<SurveyQuestion> GetQuestionByChoiceId(Int32 choiceId)
        {
            return _context.SurveyChoices
                .Where(a => a.ChoiceId == choiceId)
                .Join(_context.SurveyQuestions,
                    a => a.QuestionId,
                    b => b.QuestionId,
                    (a, b) => b)
                .ToList();
        }
        public IEnumerable<SurveyChoice> GetChoiceByChoiceId(Int32 choiceId)
        {
            return _context.SurveyChoices
                .Where(a => a.ChoiceId == choiceId)
                .ToList();
        }

    }
}
