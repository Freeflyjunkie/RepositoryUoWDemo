using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class UserRequestLog : IModelWithState
    {
        public Int32 UserRequestLogId { get; set; }
        public Int32 TransactionId { get; set; }
        public Boolean IsChangeRequest { get; set; }
        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
