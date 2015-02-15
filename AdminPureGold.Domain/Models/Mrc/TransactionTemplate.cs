using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class TransactionTemplate : IModelWithState
    {
        public Int32 TransactionTemplateId { get; set; }
        public Int32 AppObjectToTransactionId { get; set; }
        public String TemplateXml { get; set; }
        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
