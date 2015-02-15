using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.Interfaces.WeichertSL
{
    public interface IListRepository : IGenericRepository<List>
    {
        List GetListWithClosedSaleByReferenceNumber(String referenceNumber);
        IEnumerable<List> GetListsBySaleIds(IEnumerable<Int32> saleIds);
        List GetListBySaleId(Int32 saleId);
    }
}
