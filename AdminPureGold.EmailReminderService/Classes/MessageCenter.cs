using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Repositories.CorpComm;

namespace AdminPureGold.EmailReminderService.Classes
{
    public class MessageCenter
    {
        public Int32 RecipientWPersNo { get; set; }
        public Int32 SenderWPersNo { get; set; }
        public DateTime? DueDate { get; set; }
        public String SubjectText { get; set; }
        public String MessageLink { get; set; }
        public String MessageBody { get; set; }
        public Int32 Priority { get; set; }
        public bool MessageCenter_InsertMessage()
        {

            var corpCommContext = new CorpCommContext();
            var unitOfWorkCorpComm = new UnitOfWorkCorpComm(corpCommContext);
            var myCorpCommService = new CorpCommService(unitOfWorkCorpComm);

            bool rValue = false;

            try
            {
                myCorpCommService.InsertUpdate_MessageCenter(this.RecipientWPersNo, this.SenderWPersNo, this.DueDate, this.SubjectText, this.MessageLink, this.MessageBody, this.Priority);
                rValue = true;
            }
            catch
            { }

            return rValue;
        }

    }
}
