using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;

namespace AdminPureGold.WebUI.Controllers
{
    public class SandboxController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IAtlasXService _atlasXService;
        private readonly IPrintJobService _printJobService;
        private readonly IWeichertSLService _weichertslService;
        private readonly IWeichertCoreService _weichertCoreService;
        private readonly IVirtualEarthService _virtualEarthService;
        private readonly IQualityAssuranceService _qualityAssuranceService;
        private readonly IChangeRequestService _changeRequestService;

        public SandboxController(
            ITransactionService transactionService,
            IAtlasXService atlasXService,
            IPrintJobService printJobService,
            IWeichertSLService weichertSlService,
            IWeichertCoreService weichertCoreService, 
            IVirtualEarthService virtualEarthService,
            IQualityAssuranceService qualityAssuranceService,
            IChangeRequestService changeRequestService)
        {
            _transactionService = transactionService;
            _atlasXService = atlasXService;
            _printJobService = printJobService;
            _weichertslService = weichertSlService;
            _weichertCoreService = weichertCoreService;
            _virtualEarthService = virtualEarthService;
            _qualityAssuranceService = qualityAssuranceService;
            _changeRequestService = changeRequestService;
        }
        //
        // GET: /Sandbox/

        public ActionResult Index()
        {
            //var list = _weichertslService.GetListById(0);
            //var list = _weichertslService.GetListWithClosedSaleByReferenceNumber("420001848");

            //if (list != null)
            //{
            //    var sale = list.Sales.First();
            //    var officeid = sale.OfficeId;
            //    var address1  = sale.List.ListProperty.Address1;
            //}

            var startdate = new DateTime(2014, 1, 1);
            var count = _printJobService.GetPrintJobActuals(PrintJobTypeEnum.SurveyAndFollowUps, startdate, DateTime.Now);

            return View();
        }

    }
}
