using System;
using System.Threading;
using System.Collections.Generic;
using System.Web.Mvc;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.WebUI.Classes.Builders;

using AdminPureGold.Domain.Models;
using AdminPureGold.ApplicationServices.Services;

using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Repositories.Mrc;
using AdminPureGold.Repositories.Repositories.WeichertCore;

namespace AdminPureGold.WebUI.Controllers
{
    public class MessageCenterController : Controller
    {
        private readonly IToolboxService _toolboxService;

        public MessageCenterController(IToolboxService toolboxService)
        {
            _toolboxService = toolboxService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCorrectionsForPerson(Int32 personNumber)
        {
            ViewBag.PersonNumber = personNumber;
            ViewBag.AgentEmail = _toolboxService.EmailService.EmailAddressOnFile(personNumber);
            ViewBag.AgentMessage = _toolboxService.EmailService.EmailMessageForPureGoldCorrections_GetMessageForPerson(personNumber);

            return View("Index");
        }

        [HttpGet]
        public ActionResult UpdateMessageCenter(Int32 personNumber)
        {
            var myEmailSettings = _toolboxService.EmailService.GetPureGoldEmailSettings();

            ViewBag.PersonNumber = personNumber;
            ViewBag.AgentEmail = _toolboxService.EmailService.EmailAddressOnFile(personNumber);
            ViewBag.AgentMessage = _toolboxService.EmailService.EmailMessageForPureGoldCorrections_GetMessageForPerson(personNumber);

            ViewBag.MessageCenterUpdated = "";
            try
            {
                _toolboxService.CorpCommService.InsertUpdate_MessageCenter(personNumber, 0, myEmailSettings.CurrentDueDate, myEmailSettings.EmailSubject, "", myEmailSettings.MessageCenterMessage, 1);
                ViewBag.MessageCenterUpdated = "The agent's WeichertOne message center has been updated";
            }
            catch (Exception e)
            {
                ViewBag.MessageCenterUpdated = "There was an error updating the agent's WeichertOne message center - " + e.Message;
            }

            return View("Index");
        }



    }
}
