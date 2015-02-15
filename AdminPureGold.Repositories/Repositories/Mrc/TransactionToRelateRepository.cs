using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class TransactionToRelateRepository : GenericRepository<TransactionToRelate>, ITransactionToRelateRepository
    {
         private readonly MrcContext _context;

         public TransactionToRelateRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }

         public IEnumerable<TransactionToRelate> GetForPureGoldOnly(Int32 relationshipNumber)
        {
            return _context.AppObjectToTransactions
                .Where(a => a.AppObjectId == 230)
                .Join(_context.TransactionToRelates,
                    a => a.TransactionId,
                    b => b.TransactionId, 
                    (a, b) => b)
                .Where(t => t.RelationshipNumber == relationshipNumber).ToList();                                        
        }      
    }
}
