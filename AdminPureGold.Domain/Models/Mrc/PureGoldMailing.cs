using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PureGoldMailing : IModelWithState
    {
        public Int32 MailingId { get; set; }        
        public Int32 AppObjectToTransactionId { get; set; }
        public DateTime ScheduledPrintDate { get; set; }
        public DateTime? ActualPrintDate { get; set; }        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
