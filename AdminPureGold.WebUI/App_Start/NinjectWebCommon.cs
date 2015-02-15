using AdminPureGold.ApplicationServices.Interfaces;

[assembly: WebActivator.PreApplicationStartMethod(typeof(AdminPureGold.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(AdminPureGold.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace AdminPureGold.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using AdminPureGold.Repositories.EF;
    using AdminPureGold.Repositories.Interfaces.AtlasX;
    using AdminPureGold.Repositories.Repositories.AtlasX;

    using AdminPureGold.Repositories.Interfaces.CorpComm;
    using AdminPureGold.Repositories.Repositories.CorpComm;

    using AdminPureGold.Repositories.Interfaces.Mrc;
    using AdminPureGold.Repositories.Repositories.Mrc;
    using AdminPureGold.Repositories.Interfaces.WeichertCore;
    using AdminPureGold.Repositories.Repositories.WeichertCore;
    using AdminPureGold.Repositories.Interfaces.WeichertSL;
    using AdminPureGold.Repositories.Repositories.WeichertSL;
    using AdminPureGold.ApplicationServices.Services;
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IToolboxService>().To<ToolboxService>();

            // Services            
            kernel.Bind<IAtlasXService>().To<AtlasXService>();
            kernel.Bind<ITransactionService>().To<TransactionService>();
            kernel.Bind<IWeichertSLService>().To<WeichertSLService>();
            kernel.Bind<IWeichertCoreService>().To<WeichertCoreService>();
            kernel.Bind<IVirtualEarthService>().To<VirtualEarthService>();
            kernel.Bind<IQualityAssuranceService>().To<QualityAssuranceService>();
            kernel.Bind<IChangeRequestService>().To<ChangeRequestService>();
            kernel.Bind<IPrintJobService>().To<PrintJobService>();
            kernel.Bind<ISurveyService>().To<SurveyService>();
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<ICorpCommService>().To<CorpCommService>();

            // AtlasX
            kernel.Bind<IUnitOfWorkAtlasX>().To<UnitOfWorkAtlasX>();
            kernel.Bind<IPropertyRepository>().To<PropertyRepository>();
            kernel.Bind<IPropertyAlternateRepository>().To<PropertyAlternateRepository>();
            kernel.Bind<AtlasXContext>().ToSelf();

            // MRC
            kernel.Bind<IUnitOfWorkMrc>().To<UnitOfWorkMrc>();
            kernel.Bind<IAppObjectToTransactionRepository>().To<AppObjectToTransactionRepository>();            
            kernel.Bind<IChangeRequestCommentRepository>().To<ChangeRequestCommentRepository>();            
            kernel.Bind<IChangeRequestRepository>().To<ChangeRequestRepository>();            
            kernel.Bind<ILastImportDateRepository>().To<LastImportDateRepository>();
            kernel.Bind<IPrintExportNewsletterRepository>().To<PrintExportNewsletterRepository>();
            kernel.Bind<IPrintExportRepository>().To<PrintExportRepository>();
            kernel.Bind<IPrintJobRepository>().To<PrintJobRepository>();
            kernel.Bind<IPureGoldEmailRepository>().To<PureGoldEmailRepository>();
            kernel.Bind<IPureGoldMailingRepository>().To<PureGoldMailingRepository>();
            kernel.Bind<ITransactionRepository>().To<TransactionRepository>();
            kernel.Bind<ITransactionToRelateRepository>().To<TransactionToRelateRepository>();
            kernel.Bind<IUserProfileRepository>().To<UserProfileRepository>();
            kernel.Bind<IUserRequestLogRepository>().To<UserRequestLogRepository>();
            kernel.Bind<ISurveyQuestionRepository>().To<SurveyQuestionRepository>();
            kernel.Bind<ISurveyRepository>().To<SurveyRepository>();
            kernel.Bind(typeof(IMrcSqlQueryRepository<>)).To(typeof(MrcSqlQueryRepository<>));
            kernel.Bind<MrcContext>().ToSelf();

            // WeichertCore
            kernel.Bind<IUnitOfWorkCore>().To<UnitOfWorkCore>();
            kernel.Bind<IOfficeRepository>().To<OfficeRepository>();
            kernel.Bind<IPersonRepository>().To<PersonRepository>();
            kernel.Bind<IPersonToRelateRepository>().To<PersonToRelateRepository>();
            kernel.Bind<IRelateToAddressRespository>().To<RelateToAddressRepository>();
            kernel.Bind<IRelateToEmailRepository>().To<RelateToEmailRepository>();
            kernel.Bind<IRelateToNameRepository>().To<RelateToNameRepository>();
            kernel.Bind<IRelateToPhoneRepository>().To<RelateToPhoneRepository>();
            kernel.Bind<IWeichertOneUserRepository>().To<IWeichertOneUserRepository>();
            kernel.Bind(typeof(IWeichertCoreSqlQueryRepository<>)).To(typeof(WeichertCoreSqlQueryRepository<>));

            // WeichertSL
            kernel.Bind<IUnitOfWorkSalesListing>().To<UnitOfWorkSalesListing>();
            kernel.Bind<IListRepository>().To<ListRepository>();
            kernel.Bind<ISaleRepository>().To<SaleRepository>();
            kernel.Bind<WeichertSLContext>().ToSelf();

            // CorpComm
            kernel.Bind<IUnitOfWorkCorpComm>().To<UnitOfWorkCorpComm>();
            kernel.Bind<IMcMessageRepository>().To<McMessageRepository>();
            kernel.Bind<CorpCommContext>().ToSelf();

        }
    }
}
