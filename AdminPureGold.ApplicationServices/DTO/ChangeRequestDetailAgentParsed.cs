using System;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class ChangeRequestDetailAgentParsed : ChangeRequestDetailParsed
    {
        public ChangeRequestDetailAgentParsed(int changeRequestId, int changeRequestCategoryId)
        {
            ChangeRequestId = changeRequestId;
            ChangeRequestCategoryId = changeRequestCategoryId;
            PersonNumber = 0;
            RelationshipNumber = 0;
            RelateToName = new RelateToName();
            Active = String.Empty;
        }

        public Int32 PersonNumber { get; set; }
        public Int32 RelationshipNumber { get; set; }
        public Int32 OfficeId { get; set; }
        public String Active { get; set; }
        public Int32 PayAmount { get; set; }
        public Byte SortOrder { get; set; }
        public Boolean? PrintPerson { get; set; }
        public RelateToName RelateToName { get; set; }        
    }
}
