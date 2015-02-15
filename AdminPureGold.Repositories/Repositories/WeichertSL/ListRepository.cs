using System;
using System.Data.Entity;
using AdminPureGold.Domain.Models.WeichertSL;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertSL;

namespace AdminPureGold.Repositories.Repositories.WeichertSL
{
    public class ListRepository : GenericRepository<List>, IListRepository
    {
        private readonly WeichertSLContext _context;
        public ListRepository(WeichertSLContext context)
            : base(context)
        {
            _context = context;
        }
        public List GetListWithClosedSaleByReferenceNumber(string referenceNumber)
        {
            var list = _context.Lists
                .Include("ListProperty").Include("ListToAssociates").Include("ListToSellers").Include("Sales").Include("Sales.SaleToAssociates")
                .SingleOrDefault(l => l.ReferenceNumber == referenceNumber);

            if (list != null)
            {
                var sale = list.Sales
                    .Where(s => s.Closing != null).ToList();

                if (sale.Any())
                {
                    return list;
                }
            }

            return null;
        }
        public IEnumerable<List> GetListsBySaleIds(IEnumerable<int> saleIds)
        {
            return _context.Lists
                .Include("ListProperty")
                .Join(_context.Sales.Where(s => saleIds.Contains(s.SaleId)),
                    l => l.ListId,
                    s => s.ListId,
                    (l, s) => l)
                .ToList();
        }
        public List GetListBySaleId(int saleId)
        {
            //var sale = _context.Sales.SingleOrDefault(s => s.SaleId == saleId);
            //return _context.Lists
            //    .Include("ListProperty").Include("Sales").Include("Sales.SaleToBuyers").Include("Sales.SaleToAssociates")                
            //    .SingleOrDefault(l => l.ListId == sale.ListId);                
            return _context.Lists                
                .Join(_context.Sales.Where(s => s.SaleId == saleId),
                    l => l.ListId,
                    s => s.ListId,
                    (l, s) => l)
                .Include("ListProperty")
                .Include("Sales")
                .Include("Sales.SaleToBuyers")
                .Include("Sales.SaleToAssociates")
                .SingleOrDefault();
        }
    }
}
