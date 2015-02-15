using System;

namespace AdminPureGold.ApplicationServices.DTO
{
    public abstract class ChangeRequestDetailParsed
    {
        public Int32 ChangeRequestId { get; set; }
        public Int32 ChangeRequestCategoryId { get; set; }
    }
}
