using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class SurveyQuestion : IModelWithState
    {
        public Int32 QuestionId { get; set; }
        public String QuestionText { get; set; }
        public bool Active { get; set; }
        public Int32 SortOrder { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
