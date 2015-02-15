using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace AdminPureGold.Test
{
    /// <summary>
    /// Summary description for HomeControllerTest
    /// </summary>
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Home_Index()
        {
            //Mock<IUnitOfWorkMrc> moq = new Mock<IUnitOfWorkMrc>();
            //moq.Setup(u => u.TransactionRepository
            //            .Get(It.IsAny<Expression<Func<Transaction, bool>>>()
            //                , It.IsAny<Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>>()
            //                , It.IsAny<string>()))
            //    .Returns(new List<Transaction>
            //    {            
            //        new Transaction
            //        {
            //            TransactionId = 1,
            //            Active = "A",
            //            AtlasXPropertyAlternateId = 11,
            //            AtlasXPropertyId = 12,
            //            CrtDt = DateTime.Now,
            //            PersonNumber = 22200293
            //        },
            //        new Transaction
            //        {
            //            TransactionId = 2,
            //            Active = "A",
            //            AtlasXPropertyAlternateId = 22,
            //            AtlasXPropertyId = 23,
            //            CrtDt = DateTime.Now,
            //            PersonNumber = 22200293
            //        } 
            //    });

            //HomeController home = new HomeController(moq.Object);

            //moq.Verify(u => u.TransactionRepository
            //               .Get(It.IsAny<Expression<Func<Transaction, bool>>>()
            //                   , It.IsAny<Func<IQueryable<Transaction>, IOrderedQueryable<Transaction>>>()
            //                   , It.IsAny<string>()), Times.Once());

            //ActionResult result = home.Index();

            //Assert.IsNotNull(result);
        }
    }
}
