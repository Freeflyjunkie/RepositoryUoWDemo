using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class Survey : IModelWithState
    {
        public Int32 SurveyId { get; set; }
        public String ReferenceNumber { get; set; }
        public Int32? SaleId { get; set; }
        public DateTime? InputDate { get; set; }
        public String InputBy { get; set; }
        public virtual ICollection<SurveyAnswer> SurveyAnswers { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
