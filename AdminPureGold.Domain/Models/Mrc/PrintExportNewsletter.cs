using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintExportNewsletter : IModelWithState
    {
        public Int32 PureGoldId { get; set; }
        public String Associate1LastName { get; set; }
        public String Associate1Name { get; set; }
        public String Associate2Name { get; set; }
        public String OfficeName { get; set; }
        public String OfficeAddress { get; set; }
        public String OfficeCity { get; set; }
        public String OfficeState { get; set; }
        public String OfficeZip { get; set; }
        public String OfficePhone { get; set; }
        public String AssociateCell { get; set; }
        public String AssociateEmail { get; set; }
        public String Envelope { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
