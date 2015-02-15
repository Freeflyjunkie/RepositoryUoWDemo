using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PureGoldEmail : IModelWithState
    {
        public Int32 TransactionId { get; set; }
        public Int32 PersonNumber { get; set; }
        public Int32 RelationshipNumber { get; set; }
        public String ReferenceNumber { get; set; }
        public String EnvelopeName { get; set; }
        public String Address1 { get; set; }
        public String City { get; set; }
        public String AddressState { get; set; }
        public String Zipcode { get; set; }
        public Int32 AppObjectId { get; set; }
        public String PrintType { get; set; }
        public DateTime PrintDate { get; set; }
        public String AssociateName { get; set; }
        public DateTime? EmailSentDate { get; set; }
        public DateTime InputDate { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
