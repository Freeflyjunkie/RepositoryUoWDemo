using System;
using System.Collections.ObjectModel;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.AtlasX
{
    public class WAtlasX : IModelWithState
    {
        public Int64 AtlasXId { get; set; }
        public Int32 PropertyId { get; set; }
        public Int32 CreatorPersonNumber { get; set; }
        public DateTime AtlasXDate { get; set; }
        public String Active { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
        public virtual Property Property { get; set; }
        public virtual Collection<WAtlasXToApp> WAtlasXToApps { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
