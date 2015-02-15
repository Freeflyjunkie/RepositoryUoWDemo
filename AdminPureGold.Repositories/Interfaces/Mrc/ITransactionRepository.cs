
using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        IEnumerable<Transaction> GetForPureGoldOnly(Int32 personNumber);
        IEnumerable<Transaction> GetForPureGoldOnly(String customer);
        IEnumerable<Transaction> GetTransactionsByTransactionIds(IEnumerable<Int32> transactionIds);
        IEnumerable<Transaction> ListInvalidAddressTransactions();
        IEnumerable<Transaction> ListMissingDataTransactions();
        IEnumerable<Transaction> ListNoOwnershipTransactions();
        IEnumerable<Transaction> ListPrintJobFailedQaTransactions(Int32 printJobId);
        Transaction Create();
    }
}
