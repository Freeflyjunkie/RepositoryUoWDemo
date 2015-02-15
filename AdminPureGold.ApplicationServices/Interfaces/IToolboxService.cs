using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IToolboxService
    {
        IAtlasXService AtlasXService { get; }
        IChangeRequestService ChangeRequestService { get; }
        IPrintJobService PrintJobService { get; }
        IQualityAssuranceService QualityAssuranceService { get;}
        ITransactionService TransactionService { get;}
        IVirtualEarthService VirtualEarthService { get; }
        IWeichertCoreService WeichertCoreService { get; }
        IWeichertSLService WeichertSLService { get; }
        ISurveyService SurveyService { get; }
        IEmailService EmailService { get; }
        ICorpCommService CorpCommService { get; }
    }
}
