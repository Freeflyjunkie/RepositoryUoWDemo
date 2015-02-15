using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;

using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;

using AdminPureGold.ApplicationServices.Interfaces;

using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Repositories.Mrc;

using AdminPureGold.Repositories.Interfaces.WeichertCore;
using AdminPureGold.Repositories.Repositories.WeichertCore;

using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.ApplicationServices.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWorkMrc _unitOfWorkMrc;
        private readonly IUnitOfWorkCore _unitOfWorkCore;

        public EmailService(IUnitOfWorkMrc unitOfWorkMrc, IUnitOfWorkCore unitOfWorkCore)
        {
            _unitOfWorkMrc = unitOfWorkMrc;
            _unitOfWorkCore = unitOfWorkCore;
        }
        public IEnumerable<PureGoldEmail> GetPureGoldEmails()
        {
            return _unitOfWorkMrc.PureGoldEmailRepository.GetPureGoldEmails().ToList();
        }

        public IEnumerable<PureGoldEmail> GetPureGoldEmails_Sent()
        {
            return _unitOfWorkMrc.PureGoldEmailRepository.GetPureGoldEmails_Sent().ToList();
        }
        public IEnumerable<PureGoldEmail> GetPureGoldEmails_Pending()
        {
            return _unitOfWorkMrc.PureGoldEmailRepository.GetPureGoldEmails_Pending().ToList();
        }
        public PureGoldEmail GetPureGoldEmails_Pending_Next()
        {
            return _unitOfWorkMrc.PureGoldEmailRepository.GetPureGoldEmails_Pending_Next();
        }
        public int GetPureGoldEmails_Sent_Count()
        {
            return _unitOfWorkMrc.PureGoldEmailRepository.GetPureGoldEmails_Sent().Select(emails => emails.PersonNumber).Distinct().ToList().Count();
        }
        public int GetPureGoldEmails_Pending_Count()
        {
            return _unitOfWorkMrc.PureGoldEmailRepository.GetPureGoldEmails_Pending().Select(emails => emails.PersonNumber).Distinct().ToList().Count();
        }

        public PureGoldEmailSetting GetPureGoldEmailSettings()
        {
            return _unitOfWorkMrc.PureGoldEmailSettingRepository.GetPureGoldEmailSettings().SingleOrDefault();
        }

        public void LoadPureGoldEmailTable(String printType, Int32 month1, Int32 year1, Int32 month2, Int32 year2)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<PureGoldEmail>();
            mrcCoreSqlQueryRepository.LoadEmailTable(printType, month1, year1, month2, year2);
        }

        public void SaveDueDateAndSetEmailFlag(DateTime dueDate, String sendEmailsFlag)
        {
            var emailSettings = _unitOfWorkMrc.PureGoldEmailSettingRepository.GetPureGoldEmailSettings().SingleOrDefault();
            if (emailSettings != null)
            {
                emailSettings.CurrentDueDate = dueDate;
                emailSettings.SendEmailsFlag = sendEmailsFlag;
                emailSettings.EntityStateForGraphsUpdates = State.Modified;
                _unitOfWorkMrc.PureGoldEmailSettingRepository.Update(emailSettings);
            }

            _unitOfWorkMrc.Save();

        }

        public String EmailMessageForPureGoldCorrections_GetMessageForPerson(Int32 personNumber)
        {
            var myEmailSettings = GetPureGoldEmailSettings();
            var pureGoldEmailList = GetPureGoldEmails().Where(l => l.PersonNumber == personNumber).Distinct().ToList();
            var associateCustomerList = AssociateCustomerList(pureGoldEmailList);

            var associateRelateToName = _unitOfWorkCore.RelateToNameRepository.GetRelateToNameByPersonNumber(personNumber);
            var associateName = associateRelateToName.FirstName.Trim();

            string rValue = myEmailSettings.EmailBodyTop.Replace("[deadline]", myEmailSettings.CurrentDueDate.Value.ToShortDateString()).Replace("Weichert Associate", associateName) + associateCustomerList + myEmailSettings.EmailBodyBottom;

            return rValue;
        }

        /// <summary>
        /// Will Return an array with the Next Persons information.  Returns array[4] { wpersno.ToString(), emailaddress, associate first name, Email Message }
        /// </summary>
        /// <returns>
        /// array[4] { wpersno.ToString(), emailaddress, associate first name, Email Message }
        /// </returns>
        public String[] EmailMessageForPureGoldCorrections_GetNextMessage()
        {

            var myEmailSettings = GetPureGoldEmailSettings();
            var nextWPersNoFull = GetPureGoldEmails_Pending_Next();

            var nextWPersNo = nextWPersNoFull.PersonNumber;

            var pureGoldEmailList = GetPureGoldEmails_Pending().Where(l => l.PersonNumber == nextWPersNo).Distinct().ToList();

            var associateCustomerList = AssociateCustomerList(pureGoldEmailList);

            var associateRelateToName = _unitOfWorkCore.RelateToNameRepository.GetRelateToNameByPersonNumber(nextWPersNo);
            var associateName = associateRelateToName.FirstName.Trim();

            // string[] rValue = new string[4] { nextWPersNo.ToString(), "two", "three", "Four" };
            string[] rValue = new string[4] { nextWPersNo.ToString(), EmailAddressOnFile(nextWPersNo), associateName, myEmailSettings.EmailBodyTop.Replace("[deadline]", myEmailSettings.CurrentDueDate.Value.ToShortDateString()).Replace("Weichert Associate", associateName) + associateCustomerList + myEmailSettings.EmailBodyBottom };

            return rValue;
        }

        public void Update_EmailSentDate_ForWPersNo(String wPersNo)
        {
            int personNumber;
            int.TryParse(wPersNo, out personNumber);

            var emailTable = GetPureGoldEmails().Where(s => s.PersonNumber == personNumber);
            if (emailTable != null)
            {
                DateTime sentDate = DateTime.Now;
                foreach (var emailRow in emailTable)
                {
                    emailRow.EmailSentDate = sentDate;
                    emailRow.EntityStateForGraphsUpdates = State.Modified;
                    _unitOfWorkMrc.PureGoldEmailRepository.Update(emailRow);
                }
                _unitOfWorkMrc.Save();
            }
        }


        public string EmailAddressOnFile(Int32 personNumber)
        {
            var relateToEmail = _unitOfWorkCore.RelateToEmailRepository.GetRelateToEmailByPersonNumber(personNumber);
            var rValue = relateToEmail.EmailAddress;
            return rValue;
        }

        private String AssociateCustomerList(IEnumerable<PureGoldEmail> pureGoldEmailList)
        {
            StringBuilder sbList = new System.Text.StringBuilder();
            sbList.Append("<table cellpadding=\"5\" cellspacing=\"0\" border=\"1\" style=\"width:100%; Font-Family:Arial; Font-size:8pt\" width=\"100%\">");
            sbList.Append("<tr>");
            sbList.Append("<td>Customer</td>");
            sbList.Append("<td>Address</td>");
            sbList.Append("<td>Print Type</td>");
            sbList.Append("<td>Schedule Date</td>");
            sbList.Append("</tr>");
            foreach (var record in pureGoldEmailList)
            {
                sbList.Append("<tr>");
                sbList.Append("<td>" + record.EnvelopeName.Trim() + "</td>");
                sbList.Append("<td>" + record.Address1.Trim() + ", ");
                sbList.Append(" " + record.City.Trim() + " " + record.AddressState.Trim() + " " + record.Zipcode.Trim() + "</td>");
                sbList.Append("<td>" + record.PrintType.Trim() + "</td>");
                sbList.Append("<td>" + record.PrintDate.ToShortDateString() + "</td>");
                sbList.Append("</tr>");
            }
            sbList.Append("</table>");

            return sbList.ToString();

        }



    }
}
