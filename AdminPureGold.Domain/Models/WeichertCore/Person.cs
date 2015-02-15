using System;
using System.Collections.ObjectModel;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class Person
    {
        public virtual Int32 PersonNumber { get; set; }        
        public virtual DateTime? Birthday { get; set; }
        public virtual String Active { get; set; }
        public virtual String CrUser { get; set; }
        public virtual DateTime? CrDate { get; set; }
        public virtual String ChUser { get; set; }
        public virtual DateTime? ChDate { get; set; }
        public virtual Collection<PersonToImage> PersonToImage { get; set; }
        public virtual Collection<PersonToRelate> PersonToRelates { get; set; }
    }
}
