using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;
using AdminPureGold.WebUI.ViewModels;
using PagedList;

namespace AdminPureGold.WebUI.Controllers
{
    public class ReassignmentController : Controller
    {
        private readonly IToolboxService _toolboxService;
        public ReassignmentController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }
       
        public ActionResult Retrieve()
        {
            var viewModel = new List<ReassignmentViewModel>();
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetActiveAgentsByName(string search)
        {
            var nameSearch = _toolboxService.WeichertCoreService.GetAgentByLastNameOrAssociateNumber(search);
            var searchResults = nameSearch.Select(name => new
            {
                FirstName = name.LicenseFname.Trim() + " (" + name.Office.Trim() + ")",
                LastName = name.LicenseLname.Trim(),
                RelationshipNumber = name.Wrelateno,
                PersonNumber = name.Wpersno,
                OfficeId = name.OfficeId
            }).ToList();

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetActiveAndInactiveAgentsByName(string search)
        {
            var nameSearch = _toolboxService.WeichertCoreService.GetActiveInactiveAgentByLastNameOrAssociateNumber(search);
            var searchResults = nameSearch.Select(name => new
            {
                FirstName = name.LicenseFname.Trim() + " (" + name.Office.Trim() + ")",
                LastName = name.LicenseLname.Trim(),
                RelationshipNumber = name.Wrelateno,
                PersonNumber = name.Wpersno,
                name.OfficeId
            }).ToList();

            return Json(searchResults, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAgentCustomerHistory(Int32 selectedPersonNumber, Int32? page)
        {
            const int pageSize = 10;
            int pageNumber = (page ?? 1);

            var person = _toolboxService.WeichertCoreService.GetPersonByPersonNumber(selectedPersonNumber);
            var transactions = _toolboxService.TransactionService.SearchByPersonHistory(person).ToList();
            var pagedTransactions = transactions.ToPagedList(pageNumber, pageSize);

            var viewModel = ReassignmentViewModelBuilder.GetViewModels(pagedTransactions, _toolboxService);

            ViewBag.PersonNumber = selectedPersonNumber;
            var agentViewModel = AgentViewModelBuilder.GetViewModel(person, _toolboxService);
            if (agentViewModel != null)
            {
                ViewBag.PersonName = agentViewModel.RelateToName.LastName + ", " + agentViewModel.RelateToName.FirstName;
            }
            else
            {
                ViewBag.PersonName = "Unknown";
            }
            ViewBag.Total = transactions.Count();
            ViewBag.PageCount = pagedTransactions.PageCount;
            ViewBag.PageNumber = pagedTransactions.PageNumber;
            ViewBag.HasPreviousPage = pagedTransactions.HasPreviousPage;
            ViewBag.HasNextPage = pagedTransactions.HasNextPage;

            return View("Retrieve", viewModel);
        }

        public ActionResult Reassign(String transactionId, Int32 selectedPersonNumber, Int32 personNumber, Int32 relationshipNumber, Int32 officeId)
        {
            var transactionIds = transactionId.Split(',');
            foreach (var id in transactionIds)
            {
                Int32 tid;
                Int32.TryParse(id, out tid);
                if (tid != 0)
                {
                    _toolboxService.TransactionService.UpdateTransactionAgent(tid, personNumber, officeId, relationshipNumber);
                    _toolboxService.TransactionService.UpdateTransactionAgentToPrimary(tid, relationshipNumber);
                }
            }
            return RedirectToAction("GetAgentCustomerHistory", new { selectedPersonNumber = selectedPersonNumber, page = 1 });
        }
    }
}
