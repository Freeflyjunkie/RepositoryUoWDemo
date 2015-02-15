using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AdminPureGold.EmailReminderService.Classes
{
    public class Email
    {
        public string Subject { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string MessageBody { get; set; }
        public string SmtpServer { get; set; }
        public NetworkCredential SmtpCredentials { get; set; }
        public string StandaloneSendEmail(string toEmail, string toName)
        {
            string rValue = "Empty";
            try
            {
                var message = new MailMessage
                {
                    IsBodyHtml = true,
                    From = new MailAddress(this.FromEmail, this.FromName),
                    Subject = this.Subject,
                    Body = this.MessageBody
                };

                message.To.Add(new MailAddress(toEmail, toName));
                var smtp = new SmtpClient(this.SmtpServer);
                smtp.Send(message);

                rValue = "Pass";

            }
            catch (Exception ex)
            {
                rValue = ex.Message + " : " + ex.InnerException + " : " + ex.Data.ToString();
            }

            return rValue;
        }
    }
}
