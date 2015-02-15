using AdminPureGold.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class Transaction : IModelWithState
    {        
        public virtual Int32 TransactionId { get; set; }        
        public virtual Int32 PersonNumber { get; set; }
        public virtual Int32 AtlasXPropertyId { get; set; }
        public virtual DateTime CrtDt { get; set; }
        public virtual String Active { get; set; }
        public virtual Int32 AtlasXPropertyAlternateId { get; set; }
        public virtual ICollection<AppObjectToTransaction> AppObjectToTransactions { get; set; }
        public virtual ICollection<UserRequestLog> UserRequestLogs { get; set; }        
        public virtual PresentationDetail PresentationDetail { get; set; }
        public virtual ICollection<TransactionToContact> TransactionToContacts { get; set; }
        public virtual ICollection<TransactionToRelate> TransactionToRelates { get; set; }
        public virtual ICollection<ChangeRequest> ChangeRequests { get; set; } 
        public State EntityStateForGraphsUpdates { get; set; }                    
    }
}
