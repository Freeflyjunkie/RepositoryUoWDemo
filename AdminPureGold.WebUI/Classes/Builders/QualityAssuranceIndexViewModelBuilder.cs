using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPureGold.ApplicationServices.DTO.Bing;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;
using AdminPureGold.WebUI.ViewModels;
using PagedList;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class QualityAssuranceIndexViewModelBuilder
    {
        public static QualityAssuranceIndexViewModel GetViewModelWithIssues(
            int type, int page, IToolboxService toolboxService)
        {
            var viewModel = new QualityAssuranceIndexViewModel
            {
                QualityAssuranceIssues = toolboxService.QualityAssuranceService.ListQualityAssuranceIssues(),
                IssueType = (QualityAssuranceType)type,
                PropertyAlternatesPage = page,
                PropertyAlternates = Enumerable.Empty<PropertyAlternate>(),
                Transactions = Enumerable.Empty<Transaction>(),
                AgentViewModels = Enumerable.Empty<AgentViewModel>(),
                WeichertSlLists = Enumerable.Empty<List>(),
                BingLocation = new Location
                {
                    Address = String.Empty,
                    IsAddressMatch = true,
                    City = String.Empty,
                    IsCityMatch = true,
                    Confidence = String.Empty,
                    County = String.Empty,
                    State = String.Empty,
                    IsStateMatch = true,
                    Zip = String.Empty,
                    IsZipMatch = true
                },

            };

            IEnumerable<Transaction> transactionIds = toolboxService.QualityAssuranceService
                .ListTransactionsByQualityAssuranceIssueType((QualityAssuranceType)type)
                .ToList();

            var pagedTransactionIds = transactionIds.ToPagedList(page, 10);
            viewModel.Total = transactionIds.Count();
            viewModel.PageCount = pagedTransactionIds.PageCount;
            viewModel.PageNumber = pagedTransactionIds.PageNumber;
            viewModel.HasNextPage = pagedTransactionIds.HasNextPage;
            viewModel.HasPreviousPage = pagedTransactionIds.HasPreviousPage;

            viewModel.Transactions = pagedTransactionIds
                .Select(t => toolboxService.TransactionService.GetTransactionById(t.TransactionId))
                .ToList();

            // TPL Time
            var taskPropertyAlternates = Task.Factory.StartNew(() => pagedTransactionIds
                                                                    .Select(t => toolboxService.AtlasXService.GetPropertyAlternateById(t.AtlasXPropertyAlternateId))
                                                                    .ToList());

            var taskAgentViewModels = Task.Factory.StartNew(() => AgentViewModelBuilder
                                                                    .GetViewModels(viewModel.Transactions, toolboxService));

            Task.WaitAll(taskPropertyAlternates, taskAgentViewModels);

            viewModel.PropertyAlternates = taskPropertyAlternates.Result;
            viewModel.AgentViewModels = taskAgentViewModels.Result;

            return viewModel;
        }

        public static QualityAssuranceIndexViewModel GetViewModelWithoutIssues(
            int type, int page, IToolboxService toolboxService)
        {
            var viewModel = new QualityAssuranceIndexViewModel
            {
                QualityAssuranceIssues = null,
                IssueType = (QualityAssuranceType)type,
                PropertyAlternatesPage = page,
                PropertyAlternates = Enumerable.Empty<PropertyAlternate>(),
                Transactions = Enumerable.Empty<Transaction>(),
                AgentViewModels = Enumerable.Empty<AgentViewModel>(),
                WeichertSlLists = Enumerable.Empty<List>(),
                BingLocation = new Location
                {
                    Address = String.Empty,
                    IsAddressMatch = true,
                    City = String.Empty,
                    IsCityMatch = true,
                    Confidence = String.Empty,
                    County = String.Empty,
                    State = String.Empty,
                    IsStateMatch = true,
                    Zip = String.Empty,
                    IsZipMatch = true
                },

            };

            IEnumerable<Transaction> transactions = toolboxService.QualityAssuranceService
                .ListTransactionsByQualityAssuranceIssueType((QualityAssuranceType)type)
                .ToList();

            var pagedTransactions = transactions.ToPagedList(page, 10);
            viewModel.Total = transactions.Count();
            viewModel.PageCount = pagedTransactions.PageCount;
            viewModel.PageNumber = pagedTransactions.PageNumber;
            viewModel.HasNextPage = pagedTransactions.HasNextPage;
            viewModel.HasPreviousPage = pagedTransactions.HasPreviousPage;

            viewModel.Transactions = pagedTransactions
                .Select(t => toolboxService.TransactionService.GetTransactionById(t.TransactionId))
                .ToList();
            //viewModel.Transactions = transactions;

            // TPL Time
            var taskPropertyAlternates = Task.Factory.StartNew(() => pagedTransactions
                                                                    .Select(t => toolboxService.AtlasXService.GetPropertyAlternateById(t.AtlasXPropertyAlternateId))
                                                                    .ToList());

            var taskAgentViewModels = Task.Factory.StartNew(() => AgentViewModelBuilder
                                                                    .GetViewModels(viewModel.Transactions, toolboxService));

            Task.WaitAll(taskPropertyAlternates, taskAgentViewModels);

            viewModel.PropertyAlternates = taskPropertyAlternates.Result;
            viewModel.AgentViewModels = taskAgentViewModels.Result;

            return viewModel;
        }
    }
}