using System;
using System.Collections.ObjectModel;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class List
    {      
        public Int32 ListId { get; set; }
        public Int32? ReferenceNumberNumeric { get; set; }
        public Int16? ListTypeId { get; set; }
        public Int32? OfficeId { get; set; }
        public Int16? WxFlag { get; set; }
        public String ListSourceCode { get; set; }
        public Int32? ListPrice { get; set; }
        public String ListStatus { get; set; }
        public DateTime? ListEntryDate { get; set; }
        public DateTime? ListContractDate { get; set; }
        public DateTime? ListExpDate { get; set; }
        public String Municipality { get; set; }
        public String ReferenceNumber { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }

        public virtual ListProperty ListProperty { get; set; }
        public virtual Collection<ListToAssociate> ListToAssociates { get; set; }
        public virtual Collection<ListToSeller> ListToSellers { get; set; }
        public virtual Collection<Sale> Sales { get; set; }
        public virtual ListType ListType { get; set; }
        public virtual SourceCd SourceCd { get; set; }
    }
}
