using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.AtlasX
{
    public class WAtlasXtoAppWPerson : IModelWithState
    {
        public Int64 AtlasXtoAppWPersonId { get; set; }
        public Int64 AtlasXtoAppId { get; set; }
        public Int32 PersonNumber { get; set; }
        public Int32 RelationshipNumber { get; set; }
        public Int16 AtlasXtoAppRoleId { get; set; }
        public String Active { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
        public virtual WAtlasXtoAppRole WAtlasXtoAppRole { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
