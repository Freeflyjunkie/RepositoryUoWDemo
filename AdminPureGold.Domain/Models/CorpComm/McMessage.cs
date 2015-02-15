using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.CorpComm
{
    public class McMessage : IModelWithState
    {
        public Int32 MessageId { get; set; }
        public Int32 RecipientWpersno { get; set; }
        public Int32? SenderWpersno { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? AcknowledgeDate { get; set; }
        public DateTime? DueDate { get; set; }
        public Boolean DoNotDisplay { get; set; }
        public Int32? AppId { get; set; }
        public Int32? AppSubId { get; set; }
        public String SubjectText { get; set; }
        public String MessageLink { get; set; }
        public String MessageLinkTarget { get; set; }
        public Int32? OriginalMessageId { get; set; }
        public String MessageBody { get; set; }
        public Int32? Priority { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }

    }
}
