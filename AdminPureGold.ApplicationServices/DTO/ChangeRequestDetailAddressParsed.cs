using System;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class ChangeRequestDetailAddressParsed : ChangeRequestDetailParsed
    {
        public ChangeRequestDetailAddressParsed(int changeRequestId, int changeRequestCategoryId)
        {
            ChangeRequestId = changeRequestId;
            ChangeRequestCategoryId = changeRequestCategoryId;
            OldAddress1 = String.Empty;
            OldAddress2 = String.Empty;
            OldCity = String.Empty;
            OldState = String.Empty;
            OldZip = String.Empty;
            NewAddress1 = String.Empty;
            NewAddress2 = String.Empty;
            NewCity = String.Empty;
            NewState = String.Empty;
            NewZip = String.Empty;
        }

        public Int32 OldAtlasXPropertyId { get; set; }
        public Int32 OldAtlasXPropertyAlternateId { get; set; }
        public String OldAddress1 { get; set; }
        public String OldAddress2 { get; set; }
        public String OldCity { get; set; }
        public String OldState { get; set; }
        public String OldZip { get; set; }
        public Int32 NewAtlasXPropertyId { get; set; }
        public Int32 NewAtlasXPropertyAlternateId { get; set; }
        public String NewAddress1 { get; set; }
        public String NewAddress2 { get; set; }
        public String NewCity { get; set; }
        public String NewState { get; set; }
        public String NewZip { get; set; }
    }
}
