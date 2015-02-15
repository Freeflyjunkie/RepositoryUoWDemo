using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintExport : IModelWithState
    {
        public Int64? PureGoldId { get; set; }
        public String ReferenceNumber { get; set; }
        public String Rvp { get; set; }
        public String SalesAssociatesNumbers { get; set; }
        public String SalesAssociates { get; set; }
        public String SalesAssociateFirstNames { get; set; }
        public String SalesAssociateLastName { get; set; }
        public String Verb { get; set; }
        public String OfficeLongCode { get; set; }
        public String OfficeName { get; set; }
        public String OfficeAddress { get; set; }
        public String OfficeCity { get; set; }
        public String OfficeState { get; set; }
        public String OfficeZip { get; set; }
        public String Salutation { get; set; }
        public String Envelope { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
