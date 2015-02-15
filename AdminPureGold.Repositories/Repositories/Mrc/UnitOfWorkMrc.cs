using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;
using System;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class UnitOfWorkMrc : IUnitOfWorkMrc, IDisposable
    {
        private bool _disposed = false;
        private readonly MrcContext _context;
        private IAppObjectToTransactionRepository _appObjectToTransactionRepository;
        private ITransactionRepository _transactionRepository;
        private ILastImportDateRepository _lastImportDateRepository;
        private IPrintExportRepository _printExportRepository;
        private IPrintExportNewsletterRepository _printExportNewsletterRepository;
        private IPureGoldEmailRepository _pureGoldEmailRepository;
        private IPureGoldEmailSettingRepository _pureGoldEmailSettingRepository;
        private IPureGoldMailingRepository _pureGoldMailingRepository;
        private IUserProfileRepository _userProfileRepository;
        private IUserRequestLogRepository _userRequestLogRepository;
        private IChangeRequestRepository _changeRequestRepository;
        private IChangeRequestCommentRepository _changeRequestCommentRepository;
        private ITransactionToRelateRepository _transactionToRelateRepository;
        private IPrintJobRepository _printJobRepository;
        private IPrintJobToAppObjectToTransactionRepository _printJobToAppObjectToTransactionRepository;
        private ISurveyQuestionRepository _surveyQuestionRepository;
        private ISurveyRepository _surveyRepository;

        public UnitOfWorkMrc(MrcContext context)
        {
            _context = context;     
        }

        public ITransactionToRelateRepository TransactionToRelateRepository
        {
            get { return _transactionToRelateRepository ?? (_transactionToRelateRepository = new TransactionToRelateRepository(_context)); }
        }

        public IChangeRequestCommentRepository ChangeRequestCommentRepository
        {
            get { return _changeRequestCommentRepository ?? (_changeRequestCommentRepository = new ChangeRequestCommentRepository(_context)); }
        }

        public IAppObjectToTransactionRepository AppObjectToTransactionRepository
        {
            get { return _appObjectToTransactionRepository ?? (_appObjectToTransactionRepository = new AppObjectToTransactionRepository(_context)); }
        }

        public ITransactionRepository TransactionRepository
        {
            get { return _transactionRepository ?? (_transactionRepository = new TransactionRepository(_context)); }
        }

        public ILastImportDateRepository LastImportDateRepository
        {
            get { return _lastImportDateRepository ?? (_lastImportDateRepository = new LastImportDateRepository(_context)); }
        }

        public IPrintExportRepository PrintExportRepository
        {
            get { return _printExportRepository ?? (_printExportRepository = new PrintExportRepository(_context)); }
        }

        public IPrintExportNewsletterRepository PrintExportNewsletterRepository
        {
            get { return _printExportNewsletterRepository ?? (_printExportNewsletterRepository = new PrintExportNewsletterRepository(_context)); }
        }
        public IPureGoldEmailRepository PureGoldEmailRepository
        {
            get { return _pureGoldEmailRepository ?? (_pureGoldEmailRepository = new PureGoldEmailRepository(_context)); }
        }
        public IPureGoldEmailSettingRepository PureGoldEmailSettingRepository
        {
            get { return _pureGoldEmailSettingRepository ?? (_pureGoldEmailSettingRepository = new PureGoldEmailSettingRepository(_context)); }
        }

        public IPureGoldMailingRepository PureGoldMailingRepository
        {
            get { return _pureGoldMailingRepository ?? (_pureGoldMailingRepository = new PureGoldMailingRepository(_context)); }
        }

        public IUserProfileRepository UserProfileRepository
        {
            get { return _userProfileRepository ?? (_userProfileRepository = new UserProfileRepository(_context)); }
        }

        public IUserRequestLogRepository UserRequestLogRepository
        {
            get { return _userRequestLogRepository ?? (_userRequestLogRepository = new UserRequestLogRepository(_context)); }
        }

        public IChangeRequestRepository ChangeRequestRepository
        {
            get { return _changeRequestRepository ?? (_changeRequestRepository = new ChangeRequestRepository(_context)); }
        }

        public IPrintJobRepository PrintJobRepository
        {
            get { return _printJobRepository ?? (_printJobRepository = new PrintJobRepository(_context)); }
        }

        public IPrintJobToAppObjectToTransactionRepository PrintJobToAppObjectToTransactionRepository
        {
            get { return _printJobToAppObjectToTransactionRepository ?? (_printJobToAppObjectToTransactionRepository= new PrintJobToAppObjectToTransactionRepository(_context)); }
        }

        public ISurveyQuestionRepository SurveyQuestionRepository
        {
            get { return _surveyQuestionRepository ?? (_surveyQuestionRepository = new SurveyQuestionRepository(_context)); }
        }

        public ISurveyRepository SurveyRepository
        {
            get { return _surveyRepository ?? (_surveyRepository = new SurveyRepository(_context)); }
        }

        
        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


       
    }
}
