namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface IUnitOfWorkMrc : IUnitOfWork
    {
        IAppObjectToTransactionRepository AppObjectToTransactionRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        ITransactionToRelateRepository TransactionToRelateRepository { get; }
        IChangeRequestRepository ChangeRequestRepository { get; }
        IChangeRequestCommentRepository ChangeRequestCommentRepository { get; }        
        ILastImportDateRepository LastImportDateRepository { get; }
        IPrintExportRepository PrintExportRepository { get; }        
        IPrintExportNewsletterRepository PrintExportNewsletterRepository { get; }
        IPureGoldEmailRepository PureGoldEmailRepository { get; }
        IPureGoldEmailSettingRepository PureGoldEmailSettingRepository { get; }
        IPureGoldMailingRepository PureGoldMailingRepository { get; }
        IUserProfileRepository UserProfileRepository { get; }
        IUserRequestLogRepository UserRequestLogRepository { get; }
        IPrintJobRepository PrintJobRepository { get;  }
        IPrintJobToAppObjectToTransactionRepository PrintJobToAppObjectToTransactionRepository { get; }
        ISurveyQuestionRepository SurveyQuestionRepository { get; }
        ISurveyRepository SurveyRepository { get; }
    }
}
