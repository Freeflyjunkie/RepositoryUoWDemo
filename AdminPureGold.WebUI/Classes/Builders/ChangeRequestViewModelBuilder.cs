using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.WebUI.ViewModels;
using AdminPureGold.WebUI.ViewModels.Common;
using Microsoft.Win32.SafeHandles;
using PagedList;

namespace AdminPureGold.WebUI.Classes.Builders
{
    public class ChangeRequestViewModelBuilder
    {
        public static ChangeRequestViewModel GetViewModel(int changeRequestId, IToolboxService toolboxService)
        {
            var changeRequest = toolboxService.ChangeRequestService.GetChangeRequestById(changeRequestId);
            var changeRequestParsed = GetChangeRequestDetailParsed(changeRequestId, toolboxService);            
            var people = AgentViewModelBuilder.GetViewModelsFromChangeRequest(changeRequest, toolboxService);          

            return new ChangeRequestViewModel
            {
                ChangeRequests = new List<ChangeRequest> { changeRequest },
                ChangeRequestDetailParsed = new List<ChangeRequestDetailParsed> { changeRequestParsed },
                AgentViewModels = people
            };
        }
        public static ChangeRequestViewModel GetViewModelByMostRecent(int take, IToolboxService toolboxService)
        {
            var changeRequests = toolboxService.ChangeRequestService.GetChangeRequestsWithRecentComments(take).ToList();
            var changeRequestsParsed = GetChangeRequestDetailsParsed(changeRequests, toolboxService);
            var people = AgentViewModelBuilder.GetViewModelsRelateToNameFromChangeRequests(changeRequests, toolboxService);

            return new ChangeRequestViewModel
            {
                ChangeRequests = changeRequests,
                ChangeRequestDetailParsed = changeRequestsParsed,
                AgentViewModels = people
            };
        }
        public static ChangeRequestViewModel GetViewModelsByStatus(string status, IToolboxService toolboxService)
        {
            var changeRequests = toolboxService.ChangeRequestService.GetChangeRequestsByStatus(status).ToList();
            var changeRequestsParsed = GetChangeRequestDetailsParsed(changeRequests, toolboxService);
            var people = AgentViewModelBuilder.GetViewModelsRelateToNameFromChangeRequests(changeRequests, toolboxService);

            return new ChangeRequestViewModel
            {
                ChangeRequests = changeRequests,
                ChangeRequestDetailParsed = changeRequestsParsed,
                AgentViewModels = people
            };
        }
        public static ChangeRequestViewModel GetViewModelsByStatusAndCategory(string status, string category, Int32 pageNumber, IToolboxService toolboxService)
        {
            var changeRequests = toolboxService.ChangeRequestService.GetChangeRequestsByStatusAndCategory(status, category).ToList();
            var pagedChangeRequests = changeRequests.ToPagedList(pageNumber, 5);
            var changeRequestsParsed = GetChangeRequestDetailsParsed(pagedChangeRequests, toolboxService);
            var people = AgentViewModelBuilder.GetViewModelsRelateToNameFromChangeRequests(pagedChangeRequests, toolboxService);

            return new ChangeRequestViewModel
            {
                ChangeRequests = pagedChangeRequests,
                ChangeRequestDetailParsed = changeRequestsParsed,
                AgentViewModels = people,
                PageNumber = pagedChangeRequests.PageNumber,
                PageCount = pagedChangeRequests.PageCount,
                HasPreviousPage = pagedChangeRequests.HasPreviousPage,
                HasNextPage = pagedChangeRequests.HasNextPage
            };
        }
        public static IEnumerable<ChangeRequestDetailParsed> GetChangeRequestDetailsParsed(IEnumerable<ChangeRequest> changeRequests, IToolboxService toolboxService)
        {
            var changeRequestDetailParsed = new List<ChangeRequestDetailParsed>();
            foreach (var changeRequest in changeRequests)
            {
                var parsedDetail = toolboxService.ChangeRequestService.GetChangeRequestDetailParsed(changeRequest);
                if (parsedDetail != null)
                {
                    changeRequestDetailParsed.Add(parsedDetail);
                }
            }
            return changeRequestDetailParsed;
        }
        public static ChangeRequestDetailParsed GetChangeRequestDetailParsed(int changeRequestId, IToolboxService toolboxService)
        {
            return toolboxService.ChangeRequestService.GetChangeRequestDetailParsed(changeRequestId);
        }
    }
}