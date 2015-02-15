using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PresentationDetail : IModelWithState
    {                        
        public Int32 TransactionId { get; set; }
        public DateTime? PresentationDate { get; set; }
        public String CustomerName { get; set; }
        public String LeaveBehindLetterName { get; set; }
        public String PropertyType { get; set; }
        public String LotSize { get; set; }
        public String Taxes { get; set; }
        public String Lot { get; set; }
        public String Block { get; set; }
        public Int32? YearBuilt { get; set; }
        public String Renovated { get; set; }
        public String Map { get; set; }
        public String AssessLand { get; set; }
        public String AssessBuilding { get; set; }
        public String AssessTotal { get; set; }
        public String Paragraph1 { get; set; }
        public String Paragraph2 { get; set; }
        public String Room1 { get; set; }
        public String Room2 { get; set; }
        public String Room3 { get; set; }
        public String Room4 { get; set; }
        public Int32? OfferedAt { get; set; }
        public Int32 CrtBy { get; set; }
        public DateTime CrtDt { get; set; }
        public Int32? UpdBy { get; set; }
        public DateTime? UpdDt { get; set; }
        
        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
