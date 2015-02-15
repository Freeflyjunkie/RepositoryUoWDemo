using System.Collections.Generic;
using System.Linq;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.Interfaces.WeichertSL;
using AdminPureGold.Repositories.Repositories.WeichertSL;
using System;
using ServiceManager.AtlasX;

namespace AdminPureGold.ApplicationServices.Services
{
    public class WeichertSLService : IWeichertSLService
    {
        private readonly IUnitOfWorkSalesListing _unitOfWorkSalesListing;
        public WeichertSLService(IUnitOfWorkSalesListing unitOfWorkSalesListing)
        {
            _unitOfWorkSalesListing = unitOfWorkSalesListing;
        }
        public List GetListById(int listId)
        {            
            return _unitOfWorkSalesListing.ListRepository.GetById(listId);
        }
        public List GetListBySaleId(Int32 saleId)
        {
            return _unitOfWorkSalesListing.ListRepository.GetListBySaleId(saleId);
        }
        public List GetListByMrcTransactionId(Int32? mrcId)
        {           
            long? atlasXTransactionId = TransactionManager.
                GetAtlasXTransactionIDByApplicationTransactionID(TransactionManager.ApplicationName.MRC_Direct, mrcId);

            int? listId = TransactionManager.
                GetApplicationTransactionIDByAtlasXTransactionID(TransactionManager.ApplicationName.OSSII_Listing, atlasXTransactionId);

            Int32 iListId;
            Int32.TryParse(listId.ToString(), out iListId);
            return GetListById(iListId);           
        }       
        public List GetListWithClosedSaleByReferenceNumber(string referenceNumber)
        {
            return _unitOfWorkSalesListing.ListRepository.GetListWithClosedSaleByReferenceNumber(referenceNumber);
        }
        public IEnumerable<List> GetListsBySaleIds(IEnumerable<Int32> saleIds)
        {
            return _unitOfWorkSalesListing.ListRepository.GetListsBySaleIds(saleIds);
        }
        public int GetSaleIdByMrcTransactionId(int transactionId)
        {
            long? atlasXTransactionId = TransactionManager.
                GetAtlasXTransactionIDByApplicationTransactionID(TransactionManager.ApplicationName.MRC_Direct, transactionId);

            var saleId = TransactionManager.GetApplicationTransactionIDByAtlasXTransactionID(TransactionManager.ApplicationName.OSSII_Sale, atlasXTransactionId);
            return saleId ?? 0;
        }

        public List GetListWithPropertyByReferenceNumber(string referenceNumber)
        {
            return _unitOfWorkSalesListing.ListRepository.GetListWithPropertyByReferenceNumber(referenceNumber);
        }

    }
}
