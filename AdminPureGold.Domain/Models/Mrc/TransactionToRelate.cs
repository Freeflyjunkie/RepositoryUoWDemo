using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class TransactionToRelate : IModelWithState
    {
        public Int32 TransactionToRelateId { get; set; }
        public Int32 TransactionId { get; set; }
        public Int32 RelationshipNumber { get; set; }
        public Int32 PersonNumber { get; set; }
        public Int32? OfficeId { get; set; }
        public String Active { get; set; }
        public Int32 PayAmount { get; set; }
        public Byte SortOrder { get; set; }
        public Int32 CrtBy { get; set; }
        public DateTime CrtDt { get; set; }
        public Int32? UpdBy { get; set; }
        public DateTime? UpdDt { get; set; }
        public Boolean? PrintPerson { get; set; }
        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
