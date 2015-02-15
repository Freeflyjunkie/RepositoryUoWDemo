using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class ChangeRequestComment : IModelWithState
    {
        public Int32 ChangeRequestCommentId { get; set; }
        public Int32 ChangeRequestId { get; set; }
        public Int32 PersonNumber { get; set; }
        public String Comment { get; set; }        
        public DateTime? CrDate { get; set; }

        public State EntityStateForGraphsUpdates { get; set; } 
    }
}
