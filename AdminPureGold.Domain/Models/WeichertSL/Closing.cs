using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class Closing
    {
        public Int32 SaleId { get; set; }
        public Int32 FinalSalePrice { get; set; }
        public DateTime ActualCloseDate { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
    }
}
