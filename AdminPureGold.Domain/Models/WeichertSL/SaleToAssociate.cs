using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class SaleToAssociate
    {
        public Int32 SaleToAssociateId { get; set; }
        public Int32 SaleId { get; set; }
        public String SaleAssociateNumber { get; set; }
        public String AsscociateType { get; set; }
        public Int32 SaleAssociateOfficeId { get; set; }
        public Int32? PersonNumber { get; set; }
        public Int32? RelationshipNumber { get; set; }

        public virtual PureGoldSale PureGoldSale { get; set; }
    }
}
