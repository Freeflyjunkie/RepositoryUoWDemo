using System;
using System.Collections.ObjectModel;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.AtlasX
{
    public class Property : IModelWithState
    {
        public Int32 PropertyId { get; set; }
        public Int32? PropertyTypeId { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String Zip4 { get; set; }
        public String CountyName { get; set; }
        public Double? Longitude { get; set; }
        public Double? Latitude { get; set; }
        public String Block { get; set; }
        public String Lot { get; set; }
        public String FragmentHouse { get; set; }
        public String FragmentStreet { get; set; }
        public String FragmentSuffix { get; set; }
        public String FragmentPreDir { get; set; }
        public String FragmentPostDir { get; set; }
        public String FragmentUnit { get; set; }
        public String Fragment { get; set; }
        public String BarcodeDigits { get; set; }
        public Byte Validated { get; set; }
        public Boolean ValidationExempt { get; set; }
        public Int32? MatchedValidPropertyId { get; set; }
        public Int32? CRapplicationId { get; set; }
        public String CrUser { get; set; }
        public DateTime? CrDate { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
        public virtual Collection<PropertyAlternate> PropertyAlternates { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }        
    }
}
