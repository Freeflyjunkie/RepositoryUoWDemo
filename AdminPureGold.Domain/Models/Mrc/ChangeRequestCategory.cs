using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class ChangeRequestCategory : IModelWithState
    {
        public Int16 ChangeRequestCategoryId { get; set; }
        public String ChangeRequestDescription { get; set; }

        public State EntityStateForGraphsUpdates { get; set; } 

    }
}
