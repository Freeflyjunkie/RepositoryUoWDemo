using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.EmailReminderService.Classes;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Repositories.Mrc;
using AdminPureGold.Repositories.Repositories.WeichertCore;
using AdminPureGold.Repositories.Repositories.CorpComm;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceProcess;
using System.Timers;
using System.Configuration;


namespace AdminPureGold.EmailReminderService
{
    public partial class EmailReminderService : ServiceBase
    {
        private readonly System.Diagnostics.EventLog _appEventLog;
        private readonly Timer _timer;

        private int _interval = 5000;
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string SmtpServer
        {
            get { return ConfigurationManager.AppSettings["SMTPServer"] ?? Environment.MachineName + ".dmz.dmz"; }
        }

        public bool IsLiveMode
        {
            get
            {
                bool isLiveMode;
                bool.TryParse(ConfigurationManager.AppSettings["IsLiveMode"] + "", out isLiveMode);
                return isLiveMode;
            }
        }

        public EmailReminderService(System.Diagnostics.EventLog appEventLog)
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = false;
            _timer.Interval = Interval;
            _appEventLog = appEventLog;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var mrcContext = new MrcContext();
            var coreContext = new WeichertCoreContext();

            var unitOfWorkMrc = new UnitOfWorkMrc(mrcContext);
            var unitOfWorkCore = new UnitOfWorkCore(coreContext);

            var myEmailService = new EmailService(unitOfWorkMrc, unitOfWorkCore);
            var myEmailSettings = myEmailService.GetPureGoldEmailSettings();

            if (myEmailSettings.SendEmailsFlag == "1")
            {
                if (myEmailService.GetPureGoldEmails_Pending_Count() > 0)
                {
                    var myEmail = new Email();

                    var nextEmail = myEmailService.EmailMessageForPureGoldCorrections_GetNextMessage();
                    var wpersno = nextEmail[0];

                    myEmailService.Update_EmailSentDate_ForWPersNo(wpersno);

                    const string toEmail = "dlapenta@weichertrealtors.net";
                    if (IsLiveMode)
                    {
                        // toEmail = nextEmail[1].ToString();
                    }

                    var toName = nextEmail[2];
                    myEmail.MessageBody = nextEmail[3];

                    myEmail.FromName = myEmailSettings.SenderName;
                    myEmail.FromEmail = myEmailSettings.SenderEmail;
                    myEmail.Subject = myEmailSettings.EmailSubject;


                    // myEmail.smtpServer = Environment.MachineName + ".dmz.dmz";
                    // myEmail.smtpCredentials = new System.Net.NetworkCredential("", "");

                    myEmail.SmtpServer = SmtpServer;

                    // myEmail.smtpServer = "crp-mailhub02p";

                    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    var result = myEmail.StandaloneSendEmail(toEmail, toName);

                    //if (result != "Pass")
                    //{
                    //    // Send Email to Alma?  She may already get returned mail
                    //}


                    int personNumber;
                    int.TryParse(wpersno, out personNumber);
                    var myMessage = new MessageCenter
                    {
                        RecipientWPersNo = personNumber,
                        SenderWPersNo = 0,
                        DueDate = myEmailSettings.CurrentDueDate,
                        SubjectText = myEmailSettings.EmailSubject,
                        MessageLink = "",
                        MessageBody = myEmailSettings.MessageCenterMessage,
                        Priority = 1
                    };

                    myMessage.MessageCenter_InsertMessage();

                }
                else
                {
                    // Update Send Emails Flag to 0
                    myEmailService.SaveDueDateAndSetEmailFlag(DateTime.Today, "0");
                }
            }

            unitOfWorkMrc.Dispose();
        }

        protected override void OnStart(string[] args)
        {
            _timer.Enabled = true;
        }

        protected override void OnStop()
        {
            _timer.Enabled = false;
        }

    }    
}
