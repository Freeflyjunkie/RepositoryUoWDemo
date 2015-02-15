using System;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintJobToWeichertSL : IModelWithState
    {
        public Int32 PrintJobToWeichertSlId { get; set; }
        public Int32 PrintJobId { get; set; }
        public Int32 SaleId { get; set; }
        public string ReferenceNumber { get; set; }
        public Int32 PrintJobItemStatusId { get; set; }
        public virtual PrintJobItemStatus PrintJobItemStatus { get; set; }        

        public State EntityStateForGraphsUpdates { get; set; }
    }
}
