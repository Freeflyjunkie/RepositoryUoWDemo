using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class BuyerGreeting
    {
        public Int32 SaleId { get; set; }
        public String Title { get; set; }
        public String GroupName { get; set; }
        public String Salutation { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
    }
}
