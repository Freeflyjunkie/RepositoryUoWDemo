using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertSL;
using System;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IWeichertSLService
    {
        List GetListById(int id);
        List GetListBySaleId(Int32 saleId);
        List GetListByMrcTransactionId(Int32? id);
        List GetListWithClosedSaleByReferenceNumber(String referenceNumber);        
        IEnumerable<List> GetListsBySaleIds(IEnumerable<Int32> saleIds);
        Int32 GetSaleIdByMrcTransactionId(Int32 transactionId);
    }
}
