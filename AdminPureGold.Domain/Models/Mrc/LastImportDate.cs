using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class LastImportDate : IModelWithState
    {
        public Int32 LastImportDateId { get; set; }
        public DateTime ImportDate { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
