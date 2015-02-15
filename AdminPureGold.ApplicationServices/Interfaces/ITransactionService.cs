using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface ITransactionService //: //IChangeRequest//IQualityAssurance, IVirtualEarth
    {        
        Transaction GetTransactionById(int transactionId);
        Transaction GetTransactionByChangeRequestId(int changeRequestId);
        IEnumerable<Transaction> Search(String search, String searchType);
        IEnumerable<Transaction> SearchByPersonHistory(Person person);
        IEnumerable<Transaction> GetTransactionsByAppObjectToTransactionIds(IEnumerable<Int32> appObjectToTransactionIds);     
        int GetTransactionIdByChangeRequestId(int changeRequestId);
        int GetTransactionIdByAppObjectToTransactionId(int appObjectToTransactionId);        
        void UpdateTransactionProperty(int transactionId, int atlasXPropertyId, int atlasXPropertyAlternateId);
        void UpdateTransactionData(int transactionId,int personNumber, string customerName, string nameOnEnvelope);
        void UpdateTransactionAgent(int transactionId, int personNumber, int officeId, int relationshipNumber);
        void UpdateTransactionAgentStatus(int transactionId, int relationshipNumber, string status);
        void UpdateTransactionAgentToPrimary(int transactionId, int relationshipNumber);
        void UpdateTransactionAgentToSecondary(int transactionId, int relationshipNumber);
        void ActivateTransaction(int transactionId);
        void DeactivateTransaction(int transactionId);
        Int32? CreateTransactionFromListId(Int32 listId);
    }
}
