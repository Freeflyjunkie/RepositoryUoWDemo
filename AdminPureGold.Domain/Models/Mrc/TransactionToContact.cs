using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class TransactionToContact : IModelWithState
    {
        public Int32 TransactionToContactId { get; set; }
        public Int32 TransactionId { get; set; }
        public Int32? AtlasXContactId { get; set; }
        public Int32 CrtBy { get; set; }
        public DateTime CrtDt { get; set; }
        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
