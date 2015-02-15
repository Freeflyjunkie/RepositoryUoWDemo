using System;
using System.Collections.ObjectModel;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.AtlasX
{
    public class WAtlasXToApp : IModelWithState
    {        
        public Int64 AtlasXtoAppId { get; set; }
        public Int64 AtlasXId { get; set; }
        public Int32 AppNameId { get; set; }
        public Int32 AppXId { get; set; }
        public String Active { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
        public virtual WApplication WApplication { get; set; }
        public virtual Collection<WAtlasXtoAppWPerson> WAtlasXtoAppWPersons { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
