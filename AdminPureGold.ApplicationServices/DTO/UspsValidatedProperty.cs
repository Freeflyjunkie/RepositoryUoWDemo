using System;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class UspsValidatedProperty
    {     
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String ErrorCode { get; set; }
        public String DpvDescription { get; set; }
        public String DpvNotesDescription { get; set; }
        public String GeocodeLevelDescription { get; set; }
        public String CorrectionDescription { get; set; }
        public String Validated { get; set; }
    }
}
