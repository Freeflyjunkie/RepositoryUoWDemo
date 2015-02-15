using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class PureGoldSale
    {
        public Int32 SaleToAssociateId { get; set; }
        public String PureGold { get; set; }
        public Boolean PgoMissionFlag { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
    }
}
