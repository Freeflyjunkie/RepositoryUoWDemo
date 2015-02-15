using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class SurveyChoiceType : IModelWithState
    {
        public Int32 ChoiceTypeId { get; set; }
        public String ChoiceTypeDescription { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
