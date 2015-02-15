using System;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class SurveyReport
    {
        public Int32 SaleId { get; set; }
        public String ReferenceNumber { get; set; }
        public String Rvp { get; set; }
        public String OfficeName { get; set; }
        public String EnvelopeName { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String SurveyAnswerText { get; set; }
        public DateTime? InputDate { get; set; }
    }
}
