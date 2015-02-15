using System;
using System.Collections.ObjectModel;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class PersonToRelate
    {
        public virtual Int32 RelationshipNumber { get; set; }
        public virtual Int32 PersonNumber { get; set; }
        public virtual Int32 RoleTaskNumber { get; set; }
        public virtual Int32 OfficeId { get; set; }
        public virtual String Active { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? Anniversary { get; set; }
        public virtual String CrUser { get; set; }
        public virtual DateTime? CrDate { get; set; }
        public virtual String ChUser { get; set; }
        public virtual DateTime? ChDate { get; set; }
        public virtual Collection<RelateToAddress> RelateToAddresses { get; set; }
        public virtual Collection<RelateToName> RelateToNames { get; set; }
        public virtual Collection<RelateToEmail> RelateToEmails { get; set; }
        public virtual Collection<RelateToPhone> RelateToPhones { get; set; }
        public virtual Office Office { get; set; }        
    }
}
