using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintJobType : IModelWithState
    {
        public Int32 PrintJobTypeId { get; set; }
        public string PrintJobTypeDesc { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
