using System;
using System.Collections.ObjectModel;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class Sale
    {
        public Int32 SaleId { get; set; }
        public Int32 ListId { get; set; }
        public Byte SaleSequenceNumber { get; set; }
        public Int32 OfficeId { get; set; }
        public Byte? SaleTypeId { get; set; }
        public String SaleSourceCode { get; set; }
        public String SaleStatus { get; set; }
        public DateTime? SaleEntryDate { get; set; }
        public DateTime? SaleContractDate { get; set; }
        public DateTime? ScheduleCloseDate { get; set; }
        public Int32? SalePrice { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }

        public virtual BuyerGreeting BuyerGreeting { get; set; }
        public virtual Closing Closing { get; set; }        
        public virtual Collection<SaleToAssociate> SaleToAssociates { get; set; }
        public virtual Collection<SaleToBroker> SaleToBrokers { get; set; }
        public virtual Collection<SaleToBuyer> SaleToBuyers { get; set; }
        public virtual SaleType SaleType { get; set; }
        public virtual SourceCd SourceCd { get; set; }

        public virtual List List { get; set; }
    }
}
