using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class SurveyAnswerText : IModelWithState
    {
        public Int32 SurveyAnswerTextId { get; set; }
        public Int32 SurveyAnswerId { get; set; }
        public String SurveyAnswerUserText { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
