using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class ListToAssociate
    {
        public Int32 ListToAssociateId { get; set; }
        public Int32 ListId { get; set; }
        public String ListAssociateNumber { get; set; }
        public String AsscociateType { get; set; }
        public Int32? ListAssociateOfficeId { get; set; }
        public Int32? PersonNumber { get; set; }
        public Int32? RelationshipNumber { get; set; }
    }
}
