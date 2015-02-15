using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.CorpComm;
using AdminPureGold.WebUI.ViewModels;
using Microsoft.Ajax.Utilities;
using System;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class CorpCommViewModelBuilder
    {
        public static CorpCommViewModel GetMessagesForPerson(IToolboxService toolboxService, Int32 personNumber)
        {
            // CorpComm
            var taskCorpCommMessages = Task.Factory.StartNew(() => toolboxService.CorpCommService.GetMessagesForPerson(personNumber));


            // Enter last task in chain
            // Task.WaitAll(taskSurveyQuestions);//, taskWeichertCore);

            Task.WaitAll(taskCorpCommMessages);//, taskWeichertCore);

            return new CorpCommViewModel
            {
                McMessages = taskCorpCommMessages.Result
            };

        }

    }
}