using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintJobStatus : IModelWithState
    {
        public Int32 PrintJobStatusId { get; set; }
        public string PrintJobStatusDesc { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }
    }
}
