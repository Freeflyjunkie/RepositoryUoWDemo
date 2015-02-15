using System;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class RelateToEmail
    {
        public Int32 RelateToEmailId { get; set; }
        public Int32? RelationshipNumber { get; set; }
        public String EmailRoleCode { get; set; }
        public String EmailAddress { get; set; }
        public String CrUser { get; set; }
        public DateTime? CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }                
    }
}
