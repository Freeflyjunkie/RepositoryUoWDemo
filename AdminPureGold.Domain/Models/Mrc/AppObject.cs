using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class AppObject : IModelWithState
    {
        public Int32 AppObjectId { get; set; }
        public String AppObjectName { get; set; }
        public String AppObjectDesc { get; set; }
        public Int32? AppObjectTypeId { get; set; }
        public String Active { get; set; }
        public Int32 AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public virtual ICollection<AppObjectToAttribute> AppObjectToAttributes { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
