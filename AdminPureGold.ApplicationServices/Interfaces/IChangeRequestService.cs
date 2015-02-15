using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IChangeRequestService
    {
        ChangeRequest GetChangeRequestById(int changeRequestId);
        ChangeRequestDetailParsed GetChangeRequestDetailParsed(int changeRequestId);
        ChangeRequestDetailParsed GetChangeRequestDetailParsed(ChangeRequest changeRequest);
        IEnumerable<ChangeRequestComment> GetChangeRequestComments(int changeRequestId);
        IEnumerable<ChangeRequest> GetChangeRequestsWithRecentComments(int take);
        IEnumerable<ChangeRequestStatus> GetChangeRequestStatuses();
        IEnumerable<ChangeRequest> GetChangeRequestsByStatus(string statusDescription);
        IEnumerable<ChangeRequestCategory> GetChangeRequestCategories();
        IEnumerable<ChangeRequest> GetChangeRequestsByStatusAndCategory(string statusDescription, string categoryDescription);
        void UpdateChangeRequestsStatus(int changeRequestId, Int16 changeRequestStatusId);
        void UpdateChangeRequestsComment(int changeRequestId, int personNumber, string comments);
        void ChangeRequestCloseAndApply(int changeRequestId, int personNumber, ChangeRequestDetailParsed parsedDetail);
        void ChangeRequestDeny(int changeRequestId, int personNumber, string comments);
        void ChangeRequestClose(int changeRequestId, int personNumber, string comments);
    }
}
