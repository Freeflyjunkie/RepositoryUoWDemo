using System;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class RelateToAddress
    {
        public Int32 RelateToAddressId { get; set; }
        public Int32? RelationshipNumber { get; set; }
        public String AddressRoleCode { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String ZipExt { get; set; }
        public String CrUser { get; set; }
        public DateTime? CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }                
    }
}
