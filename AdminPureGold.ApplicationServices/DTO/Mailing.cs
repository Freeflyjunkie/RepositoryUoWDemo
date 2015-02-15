using System;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class Mailing
    {
        public Int32 MailingId { get; set; }
        public Int32 AppObjectToTransactionId { get; set; }
        public DateTime ScheduledPrintDate { get; set; }
        public DateTime? ActualPrintDate { get; set; }
        public String PrintType { get; set; }
    }
}
