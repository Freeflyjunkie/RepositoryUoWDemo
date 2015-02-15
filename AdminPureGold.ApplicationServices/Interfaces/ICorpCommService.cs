using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.Domain.Models.CorpComm;
using System.Linq;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface ICorpCommService
    {
        IEnumerable<McMessage> GetMessagesForPerson(Int32 personNumber);

        void InsertUpdate_MessageCenter(Int32 recipient, Int32 sender, DateTime? dueDate, String subject, String link, String body, Int32 priority);
    }
}
