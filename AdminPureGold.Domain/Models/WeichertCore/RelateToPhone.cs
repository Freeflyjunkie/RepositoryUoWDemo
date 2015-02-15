using System;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class RelateToPhone
    {
        public Int32 RelateToPhoneId { get; set; }
        public Int32? RelationshipNumber { get; set; }
        public String PhoneRoleCode { get; set; }
        public String PhoneNumber { get; set; }
        public String PhoneNumberExtension { get; set; }
        public String CrUser { get; set; }
        public DateTime? CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }                
    }
}
