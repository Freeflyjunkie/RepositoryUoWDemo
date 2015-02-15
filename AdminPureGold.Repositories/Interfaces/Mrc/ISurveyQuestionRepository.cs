using AdminPureGold.Domain.Models.Mrc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface ISurveyQuestionRepository : IGenericRepository<SurveyQuestion>
    {
        IEnumerable<SurveyQuestion> GetQuestions(Boolean active);
        IEnumerable<SurveyChoice> GetChoices(Boolean active);

        IEnumerable<SurveyQuestion> GetQuestionByChoiceId(Int32 choiceId);
        IEnumerable<SurveyChoice> GetChoiceByChoiceId(Int32 choiceId);

    }
}
