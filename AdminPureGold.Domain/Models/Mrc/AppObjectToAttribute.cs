using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class AppObjectToAttribute : IModelWithState
    {
        public Int32 AppObjectToAttributeId { get; set; }
        public Int32 AttributeTypeId { get; set; }
        public Int32 AppObjectId { get; set; }
        public String AttributeValue { get; set; }
        public Int32 AddedBy { get; set; }
        public DateTime AddedDate { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }               
    }
}
