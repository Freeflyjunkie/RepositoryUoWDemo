using System;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class ChangeRequestDetailNameParsed : ChangeRequestDetailParsed
    {
        public ChangeRequestDetailNameParsed(int changeRequestId, int changeRequestCategoryId)
        {
            ChangeRequestId = changeRequestId;
            ChangeRequestCategoryId = changeRequestCategoryId;
            OldCustomerName = String.Empty;
            OldEnvelopeName = String.Empty;
            NewCustomerName = String.Empty;
            NewEnvelopeName = String.Empty;
        }

        public String OldCustomerName { get; set; }
        public String OldEnvelopeName { get; set; }
        public String NewCustomerName { get; set; }
        public String NewEnvelopeName { get; set; }
    }
}
