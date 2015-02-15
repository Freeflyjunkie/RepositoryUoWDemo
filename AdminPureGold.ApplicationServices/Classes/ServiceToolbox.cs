using AdminPureGold.ApplicationServices.Interfaces;

namespace AdminPureGold.ApplicationServices.Classes
{
    public class ServiceToolbox
    {
        public IAtlasXService AtlasXService { get; set; }
        public IChangeRequestService ChangeRequestService { get; set; }
        public IPrintJobService PrintJobService { get; set; }
        public IQualityAssuranceService QualityAssuranceService { get; set; }
        public ITransactionService TransactionService { get; set; }
        public IVirtualEarthService VirtualEarthService { get; set; }
        public IWeichertCoreService WeichertCoreService { get; set; }
        public IWeichertSLService WeichertSLService { get; set; }
    }
}
