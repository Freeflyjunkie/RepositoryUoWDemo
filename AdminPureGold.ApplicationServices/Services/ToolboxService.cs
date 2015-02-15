using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPureGold.ApplicationServices.Interfaces;

namespace AdminPureGold.ApplicationServices.Services
{
    public class ToolboxService : IToolboxService
    {
        public ToolboxService(IAtlasXService atlasXService,
            IChangeRequestService changeRequestService,
            IPrintJobService printJobService,
            IQualityAssuranceService qualityAssuranceService,
            ITransactionService transactionService,
            IVirtualEarthService virtualEarthService,
            IWeichertCoreService weichertCoreService,
            IWeichertSLService weichertSLService,
            ISurveyService surveyService,
            IEmailService emailService,
            ICorpCommService corpCommService
            )
        {
            AtlasXService = atlasXService;
            ChangeRequestService = changeRequestService;
            PrintJobService = printJobService;
            QualityAssuranceService = qualityAssuranceService;
            TransactionService = transactionService;
            VirtualEarthService = virtualEarthService;
            WeichertCoreService = weichertCoreService;
            WeichertSLService = weichertSLService;
            SurveyService = surveyService;
            EmailService = emailService;
            CorpCommService = corpCommService;
        }
        public IAtlasXService AtlasXService { get; private set; }
        public IChangeRequestService ChangeRequestService { get; private set; }
        public IPrintJobService PrintJobService { get; private set; }
        public IQualityAssuranceService QualityAssuranceService { get; private set; }
        public ITransactionService TransactionService { get; private set; }
        public IVirtualEarthService VirtualEarthService { get; private set; }
        public IWeichertCoreService WeichertCoreService { get; private set; }
        public IWeichertSLService WeichertSLService { get; private set; }
        public ISurveyService SurveyService { get; private set; }
        public IEmailService EmailService { get; private set; }
        public ICorpCommService CorpCommService { get; private set; }
    }
}
