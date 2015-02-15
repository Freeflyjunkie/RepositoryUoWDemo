using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class SurveyAnswer : IModelWithState
    {
        public Int32 SurveyAnswerId { get; set; }
        public Int32 SurveyId { get; set; }
        public Int32 ChoiceId { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }            
    }
}
