using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Repositories.AtlasX;
using AdminPureGold.Repositories.Repositories.Mrc;
using AdminPureGold.Repositories.Repositories.WeichertCore;
using AdminPureGold.Repositories.Repositories.WeichertSL;
using AdminPureGold.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AdminPureGold.Test
{
    /// <summary>
    /// Summary description for PrintJobControllerTest
    /// </summary>
    [TestClass]
    public class PrintJobControllerTest
    {
        [TestMethod]
        public void CurrentPrintJob()
        {
            //var pjc = new PrintJobController(new IPrintJobService);
            //var cpj = pjc.CurrentPrintJob();

            //var myInt = 10;

            //var pjService = new PrintJobService(
            //    new UnitOfWorkMrc(new MrcContext()),
            //    new UnitOfWorkAtlasX(new AtlasXContext()),
            //    new UnitOfWorkCore(new WeichertCoreContext()),
            //    new UnitOfWorkSalesListing(new WeichertSLContext()));
            //pjService.GetCurrentPrintJob();

            //var mockRepository = new Mock<IPrintJobRepository>();
            //mockRepository.Setup(m => m.GetCurrentActivePrintJob()).Returns(1);
        }
    }
}
