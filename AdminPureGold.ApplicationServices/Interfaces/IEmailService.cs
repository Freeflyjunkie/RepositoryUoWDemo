using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.Domain.Models.Mrc;
using System.Linq;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IEmailService
    {
        IEnumerable<PureGoldEmail> GetPureGoldEmails();
        IEnumerable<PureGoldEmail> GetPureGoldEmails_Sent();
        IEnumerable<PureGoldEmail> GetPureGoldEmails_Pending();
        PureGoldEmail GetPureGoldEmails_Pending_Next();
        int GetPureGoldEmails_Sent_Count();
        int GetPureGoldEmails_Pending_Count();
        PureGoldEmailSetting GetPureGoldEmailSettings();        
        void LoadPureGoldEmailTable(String printType, Int32 month1, Int32 year1, Int32 month2, Int32 year2);
        void SaveDueDateAndSetEmailFlag(DateTime dueDate, String sendEmailsFlag);
        String EmailMessageForPureGoldCorrections_GetMessageForPerson(Int32 personNumber);
        String[] EmailMessageForPureGoldCorrections_GetNextMessage();
        String EmailAddressOnFile(Int32 personNumber);
    }
}
