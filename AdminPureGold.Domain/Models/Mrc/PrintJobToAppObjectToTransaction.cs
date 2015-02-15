using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintJobToAppObjectToTransaction : IModelWithState
    {
        public Int32 PrintJobToAppObjectToTransactionId { get; set; }
        public Int32 PrintJobId { get; set; }
        public Int32 AppObjectToTransactionId { get; set; }
        public Int32 PrintJobItemStatusId {get; set; }
        public virtual PrintJobItemStatus PrintJobItemStatus { get; set; }
        public virtual AppObjectToTransaction AppObjectToTransaction { get; set; }
        public virtual PrintJob PrintJob { get; set; }
        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
