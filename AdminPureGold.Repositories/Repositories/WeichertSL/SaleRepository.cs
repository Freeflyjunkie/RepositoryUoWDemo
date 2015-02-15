using System;
using System.Collections.Generic;
using System.Linq;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.WeichertSL;

namespace AdminPureGold.Repositories.Repositories.WeichertSL
{
    public class SaleRepository : GenericRepository<Sale>, ISaleRepository
    {
        private readonly WeichertSLContext _context;
        public SaleRepository(WeichertSLContext context)
            : base(context)
        {
            _context = context;
        }

        //public IEnumerable<Sale> Foo()
        //{
        //    string refNum = "164031827";


        //    var results = _context.Lists
        //        .Where(b => b.ReferenceNumber == refNum)
        //        .DefaultIfEmpty()
        //        .Join(_context.Sales,
        //            b => b.ListId,
        //            c => c.ListId,
        //            (b, c) => c)
        //        .Join(_context.Closings,
        //            d => d.SaleId,
        //            c => c.SaleId,
        //            (d, e) => e);


            
        //    return Enumerable.Empty<Sale>();

        //    //var stuff = _context.Lists
        //    //    .Where(t => t.ReferenceNumber == "")
        //    //    .Join(_context.Sales, l => l.ListId, s => s.ListId, (l, s) => new
        //    //    {
        //    //        l.ListId,                    
        //    //        l.ReferenceNumber,
        //    //        s.SaleSequenceNumber,
        //    //        s.OfficeId,
        //    //        s.SaleId
        //    //    })
        //    //    .Join(_context.Closings, ls => ls.SaleId, c => c.SaleId, (ls, c) => new
        //    //    {
        //    //        c.ActualCloseDate
        //    //    });

        //    //.Where(a => a.AppObjectId == 230)
        //    //.Join(_context.PresentationDetails,
        //    //    a => a.TransactionId,
        //    //    b => b.TransactionId,
        //    //    (a, b) => b)
        //    //.Where(t => t.CustomerName.Contains(customer) || t.LeaveBehindLetterName.Contains(customer))
        //    //.Join(_context.Transactions,
        //    //    c => c.TransactionId,
        //    //    d => d.TransactionId,
        //    //    (c, d) => d).ToList();
        //}
    }
}
