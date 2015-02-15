using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.ApplicationServices.Classes;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.Interfaces.AtlasX;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Interfaces.WeichertCore;

namespace AdminPureGold.ApplicationServices.Services
{
    public class ChangeRequestService : IChangeRequestService 
    {
        private readonly IUnitOfWorkMrc _unitOfWorkMrc;
        private readonly IUnitOfWorkAtlasX _unitOfWorkAtlasX;
        private readonly IUnitOfWorkCore _unitOfWorkCore;        

        public ChangeRequestService(IUnitOfWorkMrc unitOfWorkMrc, 
            IUnitOfWorkAtlasX unitOfWorkAtlasX, 
            IUnitOfWorkCore unitOfWorkCore)
        {
            _unitOfWorkMrc = unitOfWorkMrc;
            _unitOfWorkAtlasX = unitOfWorkAtlasX;
            _unitOfWorkCore = unitOfWorkCore;
        }

        public ChangeRequest GetChangeRequestById(int changeRequestId)
        {
            return _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
        }
        public ChangeRequestDetailParsed GetChangeRequestDetailParsed(int changeRequestId)
        {
            var changeRequest = GetChangeRequestById(changeRequestId);
            return ChangeRequestDetailParser.ParseChangeRequestDetail(changeRequest, _unitOfWorkCore);
        }

        public ChangeRequestDetailParsed GetChangeRequestDetailParsed(ChangeRequest changeRequest)
        {
            return ChangeRequestDetailParser.ParseChangeRequestDetail(changeRequest, _unitOfWorkCore);
        }
        public IEnumerable<ChangeRequestComment> GetChangeRequestComments(int changeRequestId)
        {
            return _unitOfWorkMrc.ChangeRequestCommentRepository.Get(c => c.ChangeRequestId == changeRequestId).ToList();
        }
        public IEnumerable<ChangeRequest> GetChangeRequestsWithRecentComments(int take)
        {
            var recentComments =
                _unitOfWorkMrc.ChangeRequestCommentRepository.Get(orderBy: comment => comment.OrderByDescending(c => c.CrDate)).Take(take);

            var changeRequestIds = new List<Int32>();
            foreach (var changeRequestComment in recentComments)
            {
                changeRequestIds.Add(changeRequestComment.ChangeRequestId);
            }

            return _unitOfWorkMrc.ChangeRequestRepository.GetChangeRequests(changeRequestIds);
        }
        public IEnumerable<ChangeRequestStatus> GetChangeRequestStatuses()
        {
            return _unitOfWorkMrc.ChangeRequestRepository.GetRequestStatuses();
        }
        public IEnumerable<ChangeRequest> GetChangeRequestsByStatus(string statusDescription)
        {
            return _unitOfWorkMrc.ChangeRequestRepository
                .Get(filter: t => t.ChangeRequestStatus.ChangeRequestDescription == statusDescription,
                    includeProperties: "ChangeRequestCategory,ChangeRequestStatus,ChangeRequestComments").ToList();
        }
        public IEnumerable<ChangeRequestCategory> GetChangeRequestCategories()
        {
            return _unitOfWorkMrc.ChangeRequestRepository.GetCategories();
        }
        public IEnumerable<ChangeRequest> GetChangeRequestsByStatusAndCategory(string statusDescription = "", string categoryDescription = "")
        {
            // Get All
            if (statusDescription == "" && categoryDescription == "")
            {
                return _unitOfWorkMrc.ChangeRequestRepository.Get().Take(5).ToList();
            }

            // Get By Category
            if (statusDescription == "")
            {
                return _unitOfWorkMrc.ChangeRequestRepository
                    .Get(filter: t => t.ChangeRequestCategory.ChangeRequestDescription == categoryDescription).ToList();
            }

            // Get By Status
            if (categoryDescription == "")
            {
                return _unitOfWorkMrc.ChangeRequestRepository
                    .Get(filter: t => t.ChangeRequestStatus.ChangeRequestDescription == statusDescription).ToList();
            }

            // Get By Status And Category
            return _unitOfWorkMrc.ChangeRequestRepository
                .Get(filter: t => t.ChangeRequestStatus.ChangeRequestDescription == statusDescription &&
                                  t.ChangeRequestCategory.ChangeRequestDescription == categoryDescription).ToList();
        }
        public void UpdateChangeRequestsStatus(int changeRequestId, Int16 changeRequestStatusId)
        {
            var changeRequest = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
            changeRequest.ChangeRequestStatusId = changeRequestStatusId;
            changeRequest.EntityStateForGraphsUpdates = State.Modified;
            _unitOfWorkMrc.ChangeRequestRepository.Update(changeRequest);
            _unitOfWorkMrc.Save();
        }
        public void UpdateChangeRequestsComment(int changeRequestId, int personNumber, string comments)
        {
            var changeRequestComment = new ChangeRequestComment
            {
                ChangeRequestId = changeRequestId,
                Comment = comments,
                PersonNumber = personNumber,
                CrDate = DateTime.Now,
                EntityStateForGraphsUpdates = State.Added
            };
            _unitOfWorkMrc.ChangeRequestCommentRepository.Insert(changeRequestComment);
            _unitOfWorkMrc.Save();
        }
        public void ChangeRequestClose(int changeRequestId, int personNumber, string comments)
        {
            // Add New Comment
            var changeRequestComment = new ChangeRequestComment
            {
                ChangeRequestId = changeRequestId,
                Comment = comments,
                PersonNumber = personNumber,
                CrDate = DateTime.Now,
                EntityStateForGraphsUpdates = State.Added
            };
            _unitOfWorkMrc.ChangeRequestCommentRepository.Insert(changeRequestComment);

            // Update Status
            var changeRequest = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
            changeRequest.ChangeRequestStatusId = 601;
            changeRequest.EntityStateForGraphsUpdates = State.Modified;
            _unitOfWorkMrc.ChangeRequestRepository.Update(changeRequest);

            // Save Changes
            _unitOfWorkMrc.Save();
        }
        public void ChangeRequestDeny(int changeRequestId, int personNumber, string comments)
        {
            // Add New Comment
            var changeRequestComment = new ChangeRequestComment
            {
                ChangeRequestId = changeRequestId,
                Comment = comments,
                PersonNumber = personNumber,
                CrDate = DateTime.Now,
                EntityStateForGraphsUpdates = State.Added
            };
            _unitOfWorkMrc.ChangeRequestCommentRepository.Insert(changeRequestComment);

            // Update Status
            var changeRequest = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
            changeRequest.ChangeRequestStatusId = 602;
            changeRequest.EntityStateForGraphsUpdates = State.Modified;
            _unitOfWorkMrc.ChangeRequestRepository.Update(changeRequest);

            // Save Changes
            _unitOfWorkMrc.Save();
        }
        public void ChangeRequestCloseAndApply(int changeRequestId, int personNumber, ChangeRequestDetailParsed parsedDetail)
        {
            // Update Parsed Detail
            if (parsedDetail != null)
            {
                // Get Transactions MRC and AtlasX        
                var changeRequestByTransactionId = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
                var transaction = _unitOfWorkMrc.TransactionRepository.GetById(changeRequestByTransactionId.TransactionId);

                //var transaction = GetTransactionByChangeRequestId(changeRequestId);
                var commentType = "";
                switch (parsedDetail.ChangeRequestCategoryId)
                {
                    case 400: // Address Change                    
                        ChangeRequestDetailParser.UpdateAddressWithParsedDetail(parsedDetail, transaction, _unitOfWorkMrc);
                        commentType = "address";
                        break;

                    case 401: // Agent Change                               
                        AgentManager.UpdateAgentInMrcWithParsedDetail(parsedDetail, transaction, personNumber, _unitOfWorkMrc);
                        AgentManager.UpdateAgentInAtlasXWithParsedDetail(parsedDetail, transaction, _unitOfWorkAtlasX);
                        commentType = "agent";
                        break;

                    case 402: // Name Change     
                        ChangeRequestDetailParser.UpdateCustomerNameWithParsedDetail(parsedDetail, transaction, personNumber, _unitOfWorkMrc);
                        commentType = "customer name";
                        break;

                    case 403: // Removal
                        ChangeRequestDetailParser.UpdateTransactionWithParsedDetail(transaction, _unitOfWorkMrc);
                        commentType = "removal";
                        break;

                    case 404: // Other
                        commentType = "other";
                        break;
                }

                // Add New Comment
                var changeRequestComment = new ChangeRequestComment
                {
                    ChangeRequestId = changeRequestId,
                    Comment = GetComment(transaction.PresentationDetail, commentType),
                    PersonNumber = personNumber,
                    CrDate = DateTime.Now,
                    EntityStateForGraphsUpdates = State.Added
                };

                // Update Status
                var changeRequest = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
                changeRequest.ChangeRequestStatusId = 601;
                changeRequest.EntityStateForGraphsUpdates = State.Modified;
                changeRequest.ChangeRequestComments.Add(changeRequestComment);
                _unitOfWorkMrc.ChangeRequestRepository.Update(changeRequest);

                _unitOfWorkMrc.Save();
            }
        }
        private String GetComment(PresentationDetail presentationDetail, String commentType)
        {
            // Find Customer
            var customerName = " ";
            if (presentationDetail != null)
                customerName = ", " + presentationDetail.CustomerName + ", ";

            switch (commentType)
            {
                case "other":
                    return "Your Pure Gold Customer" + customerName +
                               "has been serviced per your special request.";
                case "removal":
                    return "Your Pure Gold Customer" + customerName +
                              "has been removed.";
                default:
                    return "Your Pure Gold Customer" + customerName +
                                "has been updated with new " + commentType + " information";
            }
        }
    }
}
