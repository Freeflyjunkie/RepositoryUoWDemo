using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.AtlasX
{
    public class PropertyAlternate : IModelWithState
    {
        public Int32 PropertyAlternateId { get; set; }
        public Int32 PropertyId { get; set; }
        public Int32 PersonNumber { get; set; }
        public String AltAddress1 { get; set; }
        public String AltAddress2 { get; set; }
        public String AltCity { get; set; }
        public String AltState { get; set; }
        public String AltZip { get; set; }
        public String AltZip4 { get; set; }
        public String AltBlock { get; set; }
        public String AltLot { get; set; }
        public String CrUser { get; set; }
        public DateTime CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }        

        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
