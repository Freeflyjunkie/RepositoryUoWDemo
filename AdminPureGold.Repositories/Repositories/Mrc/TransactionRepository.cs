using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        private readonly MrcContext _context;

        public TransactionRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Transaction> GetTransactionsByTransactionIds(IEnumerable<Int32> transactionIds)
        {
            return _context.Transactions.Where(p => transactionIds.Contains(p.TransactionId)).ToList();
        }
        public IEnumerable<Transaction> GetForPureGoldOnly(Int32 personNumber)
        {
            return _context.AppObjectToTransactions
                .Where(a => a.AppObjectId == 230)
                .Join(_context.Transactions,
                    a => a.TransactionId,
                    b => b.TransactionId,
                    (a, b) => b)
                .Where(t => t.PersonNumber == personNumber).ToList();
        }
        public IEnumerable<Transaction> GetForPureGoldOnly(String customer)
        {
            return _context.AppObjectToTransactions
                .Where(a => a.AppObjectId == 230)
                .Join(_context.PresentationDetails,
                    a => a.TransactionId,
                    b => b.TransactionId,
                    (a, b) => b)
                .Where(t => t.CustomerName.Contains(customer) || t.LeaveBehindLetterName.Contains(customer))
                .Join(_context.Transactions,
                    c => c.TransactionId,
                    d => d.TransactionId,
                    (c, d) => d).ToList();
        }        
        public IEnumerable<Transaction> ListInvalidAddressTransactions()
        {
            var result = _context.Database.SqlQuery<Transaction>("exec proc_PG_Admin_QA_Select_InvalidAddress").ToList<Transaction>();
            return result.ToList();
        }
        public IEnumerable<Transaction> ListMissingDataTransactions()
        {
            var result = _context.Database.SqlQuery<Transaction>("exec proc_PG_Admin_QA_Select_MissingData").ToList<Transaction>();
            return result.ToList();
        }
        public IEnumerable<Transaction> ListNoOwnershipTransactions()
        {
            var result = _context.Database.SqlQuery<Transaction>("exec proc_PG_Admin_QA_Select_NoOwnership").ToList<Transaction>();
            return result.ToList();
        }
        public IEnumerable<Transaction> ListPrintJobFailedQaTransactions(Int32 printJobId)
        {
            var result = _context.Database.SqlQuery<Transaction>("exec proc_PG_Admin_QA_Select_PrintJob " + printJobId.ToString()).ToList<Transaction>();            
            return result.ToList();
        }
        public Transaction Create()
        {
            throw new NotImplementedException();
        }
    }
}
