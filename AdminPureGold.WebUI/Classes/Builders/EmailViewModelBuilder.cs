using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels;
using Microsoft.Ajax.Utilities;
using System;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class EmailViewModelBuilder
    {
        public static EmailViewModel GetAllMailingsInTable(IToolboxService toolboxService)
        {
            // MRC
            var taskPureGoldEmails = Task.Factory.StartNew(() => toolboxService.EmailService.GetPureGoldEmails());
            var taskPureGoldEmailSettings = taskPureGoldEmails.ContinueWith((task) => toolboxService.EmailService.GetPureGoldEmailSettings());


            // Enter last task in chain
            // Task.WaitAll(taskSurveyQuestions);//, taskWeichertCore);

            Task.WaitAll(taskPureGoldEmailSettings);//, taskWeichertCore);

            return new EmailViewModel
            {
                PureGoldEmail = taskPureGoldEmails.Result,
                PureGoldEmailSettings = taskPureGoldEmailSettings.Result
            };


        }

        public static EmailViewModel GetPureGoldEmailSettings(IToolboxService toolboxService)
        {
            // MRC
            var taskPureGoldEmailSettings = Task.Factory.StartNew(() => toolboxService.EmailService.GetPureGoldEmailSettings());

            // Enter last task in chain
            Task.WaitAll(taskPureGoldEmailSettings);//, taskWeichertCore);

            return new EmailViewModel
            {
                PureGoldEmailSettings = taskPureGoldEmailSettings.Result
            };

        }

    }
}