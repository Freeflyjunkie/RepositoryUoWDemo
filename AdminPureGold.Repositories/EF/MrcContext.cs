
using System.Data.Entity;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF.Configurations.Mrc;
using System.Diagnostics;

namespace AdminPureGold.Repositories.EF
{
    public class MrcContext : DbContext
    {
        public MrcContext()
            : base("MrcContext")
        {
            Database.SetInitializer<MrcContext>(null);
            Database.Log = sql => Debug.Write(sql);
        }
        public DbSet<LastImportDate> LastImportDates { get; set; }
        public DbSet<PrintExport> PrintExports { get; set; }
        public DbSet<PrintExportNewsletter> PrintExportNewsletters { get; set; }
        public DbSet<PureGoldEmail> PureGoldEmails { get; set; }
        public DbSet<PureGoldEmailSetting> PureGoldEmailSettings { get; set; }
        public DbSet<PureGoldMailing> PureGoldMailings { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AppObject> AppObjects { get; set; }
        public DbSet<AppObjectToTransaction> AppObjectToTransactions { get; set; }
        public DbSet<UserProfile> UerProfiles { get; set; }
        public DbSet<UserRequestLog> UserRequestLogs { get; set; }
        public DbSet<ChangeRequest> ChangeRequests { get; set; }
        public DbSet<ChangeRequestComment> ChangeRequestComments { get; set; }
        public DbSet<ChangeRequestCategory> ChangeRequestCategories { get; set; }
        public DbSet<ChangeRequestStatus> ChangeRequestStatuses { get; set; }
        public DbSet<PresentationDetail> PresentationDetails { get; set; }
        public DbSet<TransactionToRelate> TransactionToRelates { get; set; }
        public DbSet<PrintJob> PrintJobs { get; set; }
        public DbSet<PrintJobType> PrintJobTypes { get; set; }
        public DbSet<PrintJobStatus> PrintJobStatuses { get; set; }
        public DbSet<PrintJobToAppObjectToTransaction> PrintJobToAppObjectToTransactions { get; set; }
        public DbSet<PrintJobToWeichertSL> PrintJobToWeichertSLs { get; set; }
        public DbSet<PrintJobItemStatus> PrintJobItemStatuses { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyAnswerText> SurveyAnswerTexts { get; set; }
        public DbSet<SurveyUserSetting> SurveyUserSettings { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyChoice> SurveyChoices { get; set; }
        public DbSet<SurveyChoiceType> SurveyChoiceTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppObjectConfiguration());
            modelBuilder.Configurations.Add(new AppObjectToAttributeConfiguration());
            modelBuilder.Configurations.Add(new AppObjectToTransactionConfiguration());

            modelBuilder.Configurations.Add(new LastImportDateConfiguration());
            modelBuilder.Configurations.Add(new PresentationDetailConfiguration());

            modelBuilder.Configurations.Add(new PrintExportConfiguration());
            modelBuilder.Configurations.Add(new PrintExportNewsletterConfiguration());
            modelBuilder.Configurations.Add(new PureGoldEmailConfiguration());
            modelBuilder.Configurations.Add(new PureGoldEmailSettingsConfiguration());
            modelBuilder.Configurations.Add(new PureGoldMailingConfiguration());

            modelBuilder.Configurations.Add(new TransactionConfiguration());
            modelBuilder.Configurations.Add(new TransactionTemplateConfiguration());
            modelBuilder.Configurations.Add(new TransactionToContactConfiguration());
            modelBuilder.Configurations.Add(new TransactionToRelateConfiguration());

            modelBuilder.Configurations.Add(new UserProfileConfiguration());
            modelBuilder.Configurations.Add(new UserRequestLogConfiguration());

            modelBuilder.Configurations.Add(new ChangeRequestConfiguration());
            modelBuilder.Configurations.Add(new ChangeRequestCategoryConfiguration());
            modelBuilder.Configurations.Add(new ChangeRequestStatusConfiguration());
            modelBuilder.Configurations.Add(new ChangeRequestCommentConfiguration());

            modelBuilder.Configurations.Add(new PrintJobConfiguration());
            modelBuilder.Configurations.Add(new PrintJobTypeConfiguration());
            modelBuilder.Configurations.Add(new PrintJobStatusConfiguration());
            modelBuilder.Configurations.Add(new PrintJobToAppObjectToTransactionConfiguration());
            modelBuilder.Configurations.Add(new PrintJobToWeichertSLConfiguration());
            modelBuilder.Configurations.Add(new PrintJobItemStatusConfiguration());

            modelBuilder.Configurations.Add(new SurveyConfiguration());
            modelBuilder.Configurations.Add(new SurveyAnswerConfiguration());
            modelBuilder.Configurations.Add(new SurveyAnswerTextConfiguration());
            modelBuilder.Configurations.Add(new SurveyUserSettingConfiguration());

            modelBuilder.Configurations.Add(new SurveyQuestionConfiguration());
            modelBuilder.Configurations.Add(new SurveyChoiceConfiguration());
            modelBuilder.Configurations.Add(new SurveyChoiceTypeConfiguration());
        }
    }
}
