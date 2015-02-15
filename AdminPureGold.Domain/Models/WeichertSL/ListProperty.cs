using System;

namespace AdminPureGold.Domain.Models.WeichertSL
{
    public class ListProperty
    {
        public Int32 ListId { get; set; }
        public Int32? AtlasXPropertyId { get; set; }
        public String  Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Block { get; set; }
        public String Lot { get; set; }
        public String ChUser { get; set; }
        public DateTime? ChDate { get; set; }
    }
}
