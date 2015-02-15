using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintJobItemStatus : IModelWithState
    {
        public Int32 PrintJobItemStatusId { get; set; }
        public string PrintJobItemStatusDesc { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }
    }
}
