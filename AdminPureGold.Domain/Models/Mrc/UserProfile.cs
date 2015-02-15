using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class UserProfile : IModelWithState
    {        
        public Int32 ProfileUserId { get; set; }
        public Int32 PersonNumber { get; set; }
        public Int32 RelationshipNumber { get; set; }
        public Int32 XWrNetId { get; set; }
        public String XAscNum { get; set; }
        public Int32 RelateToNameId { get; set; }
        public Int32 RelateToPhoneId { get; set; }
        public Int32 RelateToEmailId { get; set; }
        public Int32 AssociateTitleId { get; set; }
        public String Website { get; set; }
        public Boolean ShowMiddleInitial { get; set; }
        public Boolean ShowCommonName { get; set; }
        public Boolean ShowTitle { get; set; }
        public Boolean ShowSuffix { get; set; }
        public Boolean ShowPhone { get; set; }
        public Boolean ShowEmail { get; set; }
        public Boolean ShowWebsite { get; set; }
        public Int32 CrtBy { get; set; }
        public DateTime CrtDt { get; set; }
        public Int32 UpdBy { get; set; }
        public DateTime UpdDt { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
