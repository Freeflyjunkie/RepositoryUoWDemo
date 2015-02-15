using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class SaleToBuyer
    {
        public Int32 SaleToBuyerId { get; set; }
        public Int32 SaleId { get; set; }
        public Byte BuyerSequenceNumber { get; set; }
        public String Title { get; set; }
        public String FirstName { get; set; }
        public String MiddleInitial { get; set; }
        public String LastName { get; set; }
        public String Suffix { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String Country { get; set; }
        public String HomePhone { get; set; }
        public String BusinessPhone { get; set; }
        public String Email { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
    }
}
