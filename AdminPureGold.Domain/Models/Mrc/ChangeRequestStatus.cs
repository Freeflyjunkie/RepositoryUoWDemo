using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class ChangeRequestStatus : IModelWithState
    {
        public Int16 ChangeRequestStatusId { get; set; }
        public String ChangeRequestDescription { get; set; }

        public State EntityStateForGraphsUpdates { get; set; } 
    }
}
