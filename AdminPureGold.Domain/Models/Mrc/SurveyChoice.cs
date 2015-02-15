using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class SurveyChoice : IModelWithState
    {
        public Int32 ChoiceId { get; set; }
        public Int32 QuestionId { get; set; }
        public Int32 ChoiceTypeId { get; set; }
        public String ChoiceText { get; set; }
        public Int32 SortOrder { get; set; }
        public bool Active { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
