
using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.ApplicationServices.Classes;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;
using AdminPureGold.Repositories.Interfaces.AtlasX;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Interfaces.WeichertCore;
using AdminPureGold.Repositories.Interfaces.WeichertSL;
using AdminPureGold.Repositories.Repositories.Mrc;
using AdminPureGold.Repositories.Repositories.WeichertCore;
using ServiceManager.AtlasX;

namespace AdminPureGold.ApplicationServices.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWorkMrc _unitOfWorkMrc;
        private readonly IUnitOfWorkAtlasX _unitOfWorkAtlasX;
        private readonly IUnitOfWorkSalesListing _unitOfWorkSalesListing;
        private const int NegativeOne = -1;

        public TransactionService(IUnitOfWorkMrc unitOfWorkMrc, IUnitOfWorkAtlasX unitOfWorkAtlasX, IUnitOfWorkSalesListing unitOfWorkSalesListing)
        {
            _unitOfWorkMrc = unitOfWorkMrc;
            _unitOfWorkAtlasX = unitOfWorkAtlasX;
            _unitOfWorkSalesListing = unitOfWorkSalesListing;
        }

        #region ITransactionService
        public Transaction GetTransactionById(int transactionId)
        {
            return _unitOfWorkMrc.TransactionRepository.GetById(transactionId);
        }
        public Transaction GetTransactionByChangeRequestId(int changeRequestId)
        {
            var changeRequest = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
            return _unitOfWorkMrc.TransactionRepository.GetById(changeRequest.TransactionId);
        }
        public IEnumerable<Transaction> Search(String search, String searchType)
        {
            var transactions = new List<Transaction>();
            var weichertCoreSqlQueryRepository = new WeichertCoreSqlQueryRepository<ViewBaseAssociateActive>();

            switch (searchType)
            {
                case "1": // TransactionId
                    Int32 transactionId;
                    Int32.TryParse(search, out transactionId);                    
                    return _unitOfWorkMrc.TransactionRepository.Get(t => t.TransactionId == transactionId);

                case "2": // Reference Number
                    var lists = _unitOfWorkSalesListing.ListRepository.Get(l => l.ReferenceNumber == search);

                    foreach (var list in lists)
                    {
                        long? atlasXTransactionId = TransactionManager.
                            GetAtlasXTransactionIDByApplicationTransactionID(TransactionManager.ApplicationName.OSSII_Listing, list.ListId);

                        int? mrcTransactionId = TransactionManager.
                            GetApplicationTransactionIDByAtlasXTransactionID(TransactionManager.ApplicationName.MRC_Direct, atlasXTransactionId);

                        if (mrcTransactionId != null)
                        {
                            if (mrcTransactionId != 0)
                            {
                                var transaction = _unitOfWorkMrc.TransactionRepository.GetById(mrcTransactionId);
                                transactions.Add(transaction);
                            }
                        }
                    }
                    return transactions;

                case "3": // Customer Name
                    return _unitOfWorkMrc.TransactionRepository.GetForPureGoldOnly(search);

                case "4": // Agent Name  or Associate Number                  
                    var agents = weichertCoreSqlQueryRepository.GetAgentByLastNameOrAssociateNumber(search);

                    foreach (var agent in agents)
                    {
                        ViewBaseAssociateActive localAgent = agent;

                        var ownedTransactions =
                            _unitOfWorkMrc.TransactionRepository.GetForPureGoldOnly(localAgent.Wpersno);

                        foreach (var transaction in ownedTransactions)
                        {
                            transactions.Add(transaction);
                        }

                        var transactionToRelates = _unitOfWorkMrc.TransactionToRelateRepository.GetForPureGoldOnly(localAgent.Wrelateno).ToList();
                        var transactionIds = transactionToRelates.Select(tranactionToRelate => tranactionToRelate.TransactionId).ToList();
                        var relatedTransactions = _unitOfWorkMrc.TransactionRepository.GetTransactionsByTransactionIds(transactionIds);

                        foreach (var transaction in relatedTransactions)
                        {
                            if (!transactions.Any(t => t.TransactionId == transaction.TransactionId))
                            {
                                transactions.Add(transaction);
                            }
                        }
                    }
                    return transactions;
            }
            return new List<Transaction>();
        }
        public IEnumerable<Transaction> SearchByPersonHistory(Person person)
        {
            var transactions = new List<Transaction>();

            var ownedTransactions = _unitOfWorkMrc.TransactionRepository.GetForPureGoldOnly(person.PersonNumber);
            //var ownedTransactions = _unitOfWorkMrc.TransactionRepository.Get(t => t.PersonNumber == person.PersonNumber);

            foreach (var transaction in ownedTransactions)
            {
                transactions.Add(transaction);
            }

            foreach (var personToRelates in person.PersonToRelates)
            {
                var localPersonToRelates = personToRelates;
                var transactionToRelates = _unitOfWorkMrc.TransactionToRelateRepository.GetForPureGoldOnly(localPersonToRelates.RelationshipNumber);
                //var transactionToRelates = _unitOfWorkMrc.TransactionToRelateRepository.Get(t => t.PersonNumber == localPersonToRelates.PersonNumber);
                var transactionIds = transactionToRelates.Select(tranactionToRelate => tranactionToRelate.TransactionId).ToList();
                var relatedTransactions = _unitOfWorkMrc.TransactionRepository.GetTransactionsByTransactionIds(transactionIds);

                foreach (var transaction in relatedTransactions)
                {
                    if (!transactions.Any(t => t.TransactionId == transaction.TransactionId))
                    {
                        transactions.Add(transaction);
                    }
                }
            }

            return transactions;
        }
        public IEnumerable<Transaction> GetTransactionsByAppObjectToTransactionIds(IEnumerable<Int32> appObjectToTransactionIds)
        {
            var appObjectToTransactions = _unitOfWorkMrc.AppObjectToTransactionRepository
                .Get(t => appObjectToTransactionIds.Contains(t.AppObjectToTransactionId));

            var transactionIds = appObjectToTransactions
                .Select(appObjectToTransaction => appObjectToTransaction.TransactionId).ToList();

            return _unitOfWorkMrc.TransactionRepository.GetTransactionsByTransactionIds(transactionIds);
        }
        public int GetTransactionIdByChangeRequestId(int changeRequestId)
        {
            ChangeRequest changeRequest = _unitOfWorkMrc.ChangeRequestRepository.GetById(changeRequestId);
            return changeRequest.TransactionId;
        }
        public int GetTransactionIdByAppObjectToTransactionId(int appObjectToTransactionId)
        {
            AppObjectToTransaction appObjectToTransaction =
                _unitOfWorkMrc.AppObjectToTransactionRepository.Get(
                    a => a.AppObjectToTransactionId == appObjectToTransactionId).SingleOrDefault();

            return appObjectToTransaction == null ? 0 : appObjectToTransaction.TransactionId;
        }
        public void UpdateTransactionProperty(int transactionId, int atlasXPropertyId, int atlasXPropertyAlternateId)
        {
            if (atlasXPropertyId != NegativeOne && atlasXPropertyAlternateId != NegativeOne)
            {
                Transaction transaction = _unitOfWorkMrc.TransactionRepository.GetById(transactionId);
                transaction.AtlasXPropertyId = atlasXPropertyId;
                transaction.AtlasXPropertyAlternateId = atlasXPropertyAlternateId;

                transaction.EntityStateForGraphsUpdates = State.Modified;
                _unitOfWorkMrc.TransactionRepository.Update(transaction);
                _unitOfWorkMrc.Save();
            }
        }
        public void UpdateTransactionData(int transactionId, int personNumber, string customerName, string nameOnEnvelope)
        {
            if (customerName != String.Empty && nameOnEnvelope != String.Empty)
            {
                var transaction = _unitOfWorkMrc.TransactionRepository.GetById(transactionId);
                if (transaction.PresentationDetail != null)
                {
                    transaction.PresentationDetail.CustomerName = customerName;
                    transaction.PresentationDetail.LeaveBehindLetterName = nameOnEnvelope;
                    transaction.PresentationDetail.EntityStateForGraphsUpdates = State.Modified;
                }
                else
                {
                    var presentationDetail = new PresentationDetail
                    {
                        TransactionId = transaction.TransactionId,
                        CustomerName = customerName,
                        LeaveBehindLetterName = nameOnEnvelope,
                        CrtBy = personNumber,
                        CrtDt = DateTime.Now,
                        EntityStateForGraphsUpdates = State.Added
                    };
                    transaction.PresentationDetail = presentationDetail;
                }

                _unitOfWorkMrc.TransactionRepository.Update(transaction);
                _unitOfWorkMrc.Save();
            }
        }
        public void UpdateTransactionAgent(int transactionId, int personNumber, int officeId, int relationshipNumber)
        {
            AgentManager.UpdateAgentInMrc(transactionId, personNumber, relationshipNumber, officeId, _unitOfWorkMrc);
            AgentManager.UpdateAgentInAtlasX(transactionId, personNumber, relationshipNumber, _unitOfWorkAtlasX);
        }
        public void UpdateTransactionAgentStatus(int transactionId, int relationshipNumber, string status)
        {
            AgentManager.UpdateAgentStatusInMrc(transactionId, relationshipNumber, status, _unitOfWorkMrc);
            AgentManager.UpdateAgentStatusInAtlasX(transactionId, relationshipNumber, status, _unitOfWorkAtlasX);
        }
        public void UpdateTransactionAgentToPrimary(int transactionId, int relationshipNumber)
        {
            AgentManager.UpdateAgentToPrimaryInMrc(transactionId, relationshipNumber, _unitOfWorkMrc);
            AgentManager.UpdateAgentToPrimaryInAtlasX(transactionId, relationshipNumber, _unitOfWorkAtlasX);
        }
        public void UpdateTransactionAgentToSecondary(int transactionId, int relationshipNumber)
        {
            AgentManager.UpdateAgentToSecondaryInMrc(transactionId, relationshipNumber, _unitOfWorkMrc);
            AgentManager.UpdateAgentToSecondaryInAtlasX(transactionId, relationshipNumber, _unitOfWorkAtlasX);
        }
        public void ActivateTransaction(int transactionId)
        {
            var transaction = _unitOfWorkMrc.TransactionRepository.GetById(transactionId);
            transaction.Active = "A";
            transaction.EntityStateForGraphsUpdates = State.Modified;
            _unitOfWorkMrc.TransactionRepository.Update(transaction);
            _unitOfWorkMrc.Save();
        }
        public void DeactivateTransaction(int transactionId)
        {
            var transaction = _unitOfWorkMrc.TransactionRepository.GetById(transactionId);
            transaction.Active = "I";
            transaction.EntityStateForGraphsUpdates = State.Modified;
            _unitOfWorkMrc.TransactionRepository.Update(transaction);
            _unitOfWorkMrc.Save();
        }
        public Int32? CreateTransactionFromListId(int listId)
        {
            var mrcSqlQueryRepository = new MrcSqlQueryRepository<CreateTransaction>();
            return mrcSqlQueryRepository.CreateTransactionFromListId(listId);
            //return transaction.TransactionID;
        }
        #endregion
    }
}
