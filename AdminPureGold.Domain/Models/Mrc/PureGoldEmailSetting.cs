using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PureGoldEmailSetting : IModelWithState
    {
        public Int32 EmailSettingId { get; set; }
        public String SendEmailsFlag { get; set; }
        public String EmailSubject { get; set; }
        public String EmailBodyTop { get; set; }
        public String EmailBodyBottom { get; set; }
        public String MessageCenterMessage { get; set; }
        public DateTime? CurrentDueDate { get; set; }
        public String SenderName { get; set; }
        public String SenderEmail { get; set; }
        public String EmailServer { get; set; }
        public String ErrorRecipients { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
