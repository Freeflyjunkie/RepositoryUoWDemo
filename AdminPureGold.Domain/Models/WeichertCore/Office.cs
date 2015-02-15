using System;

namespace AdminPureGold.Domain.Models.WeichertCore
{
    public class Office
    {
        public Int32 OfficeId { get; set; }
        public String OfficeLongNumber { get; set; }
        public String Division { get; set; }
        public String DivisionName { get; set; }
        public String Region { get; set; }
        public String Rvp { get; set; }
        public String OfficeNumber { get; set; }
        public String OfficeName { get; set; }
        public String AbbreviatedName { get; set; }
        public String State { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String ZipCode { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        public String OfficeType { get; set; }
        public String OfficeStatus { get; set; }
        public String County { get; set; }
        public String Directions { get; set; }
        public String FriendlyOfficeName { get; set; }
        public String FriendlyDivisionName { get; set; }        
    }
}
