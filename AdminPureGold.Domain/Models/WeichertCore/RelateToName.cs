using System;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class RelateToName
    {
        public Int32 RelateToNameId { get; set; }
        public Int32 RelationshipNumber { get; set; }
        public String NameRoleCode { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public String Suffix { get; set; }
        public String CrUser { get; set; }
        public DateTime? CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }        
    }
}
