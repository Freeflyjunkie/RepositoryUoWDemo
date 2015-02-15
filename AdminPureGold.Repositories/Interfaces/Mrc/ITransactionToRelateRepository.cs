using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{    
    public interface ITransactionToRelateRepository : IGenericRepository<TransactionToRelate>
    {
        IEnumerable<TransactionToRelate> GetForPureGoldOnly(Int32 relationshipNumber);
    }
}
