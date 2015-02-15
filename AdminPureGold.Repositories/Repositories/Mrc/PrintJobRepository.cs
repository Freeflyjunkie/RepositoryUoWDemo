using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.EF;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.Repositories.Repositories.Mrc
{
    public class PrintJobRepository : GenericRepository<PrintJob>, IPrintJobRepository
    {
        private readonly MrcContext _context;

        public PrintJobRepository(MrcContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Int32> GetAppObjectToTransactionIdsByPrintJobId(Int32 printJobId, Int32? status)
        {
            return status == null ?
               _context.PrintJobToAppObjectToTransactions
               .Where(item => item.PrintJobId == printJobId)
               .Select(t => t.AppObjectToTransactionId).ToList() :

               _context.PrintJobToAppObjectToTransactions
               .Where(item => item.PrintJobId == printJobId && item.PrintJobItemStatusId == status)
               .Select(t => t.AppObjectToTransactionId).ToList();
        }
        public IEnumerable<PrintJobType> GetPrintJobTypes()
        {
            return _context.PrintJobTypes.ToList();
        }
        public IEnumerable<PrintJobItemStatus> GetPrintJobItemStatuses()
        {
            return _context.PrintJobItemStatuses.ToList();
        }
        public Int32 GetPrintJobToAppObjectToTransactionsCount(Int32 printJobId, Int32? status)
        {
            return status == null ?
                _context.PrintJobToAppObjectToTransactions.Count(p => p.PrintJobId == printJobId) :
                _context.PrintJobToAppObjectToTransactions.Count(p => p.PrintJobId == printJobId && p.PrintJobItemStatusId == status);
        }
        public Int32 GetPrintJobToWeichertSLCount(Int32 printJobId, Int32? status)
        {
            return status == null ?
                _context.PrintJobToWeichertSLs.Count(p => p.PrintJobId == printJobId) :
                _context.PrintJobToWeichertSLs.Count(p => p.PrintJobId == printJobId && p.PrintJobItemStatusId == status);
        }
        public IEnumerable<PrintJobToWeichertSL> GetPrintJobToWeichertSLsByPrintJobToWeichertSLIds(IEnumerable<Int32> printJobToWeichertSLIds)
        {
            return _context.PrintJobToWeichertSLs
                .Include("PrintJobItemStatus")
                .Where(t => printJobToWeichertSLIds.Contains(t.PrintJobToWeichertSlId)).ToList();
        }
        public IEnumerable<Int32> GetPrintJobToWeichertSLIds(int printJobId, Int32? printJobItemStatus)
        {
            var pjItems = printJobItemStatus == null
                ? _context.PrintJobToWeichertSLs.Where(t => t.PrintJobId == printJobId)
                    .Select(p => p.PrintJobToWeichertSlId)
                : _context.PrintJobToWeichertSLs.Where(t => t.PrintJobId == printJobId && t.PrintJobItemStatusId == printJobItemStatus)
                    .Select(p => p.PrintJobToWeichertSlId);

            return pjItems.ToList();
        }
        public PrintJobToAppObjectToTransaction GetPrintJobToAppObjectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId)
        {
            return _context.PrintJobToAppObjectToTransactions.SingleOrDefault(
                    t => t.PrintJobId == printJobId && t.AppObjectToTransactionId == appObjectToTransactionId);
        }
        public IEnumerable<Int32> GetPrintJobAppObjectToTransactionIds(Int32 printJobId, Int32? printJobItemStatus)
        {
            var pjItems = printJobItemStatus == null
               ? _context.PrintJobToAppObjectToTransactions.Where(t => t.PrintJobId == printJobId)
                   .Select(p => p.PrintJobToAppObjectToTransactionId)
               : _context.PrintJobToAppObjectToTransactions.Where(t => t.PrintJobId == printJobId && t.PrintJobItemStatusId == printJobItemStatus)
                   .Select(p => p.PrintJobToAppObjectToTransactionId);

            return pjItems.ToList();
        }
        public IEnumerable<PrintJobToAppObjectToTransaction> GetPrintJobToAppObjectToTransactionsByAppObjectToTransactionIds
            (Int32? printJobId, IEnumerable<Int32> appObjectToTransactionIds)
        {
            return _context.PrintJobs.Where(pj => pj.PrintJobId == printJobId)
                .Join(_context.PrintJobToAppObjectToTransactions.Where(item => appObjectToTransactionIds.Contains(item.AppObjectToTransactionId)),
                    pj => pj.PrintJobId,
                    item => item.PrintJobId,
                    (pj, item) => item)
                .Include("PrintJobItemStatus")
                .ToList();
        }
        public int GetPrintJobActuals(int type, DateTime startdate, DateTime enddate)
        {
            Expression<Func<AppObject, bool>> predicate = a => a.AppObjectName.Contains("ThankYou");
            switch (type)
            {
                case 10: // Thank You
                    predicate = a => a.AppObjectId == 181; //T_PG_ThankYou
                    break;
                case 11: // Surveys Per Dan Use 182 Only (Same Envelope and Stamp) 
                    predicate = a => a.AppObjectId == 182; //T_PG_FollowUp or T_PG_Survey(a.AppObjectId == 183)
                    break;
                case 12: // Anniversary
                    predicate = a => a.AppObjectName.Contains("Anniversary"); //T_PG_Anniversary1234 or T_PG_Renew_Anniversary1234
                    break;
                case 13: // Spring Newsletters
                    predicate = a => a.AppObjectName.Contains("SpringSummerNewsletter"); // T_PG_SpringSummerNewsletter12345 or T_PG_Renew_SpringSummerNewsletter12345
                    break;
                case 14: // Fall Newsletters
                    predicate = a => a.AppObjectName.Contains("FallWinterNewsletter"); // T_PG_FallWinterNewsletter12345 or T_PG_Renew_FallWinterNewsletter12345
                    break;
                case 15: // Surveys Non-Pure Gold
                    var nonPgMailings = _context.PrintJobs
                        .Include("PrintJobToWeichertSLs")
                        .Where(pj => pj.PrintJobTypeId == type
                            && pj.PrintJobStatusId == 4 // Marked As Successful
                            && pj.UpdDt >= startdate
                            && pj.UpdDt <= enddate).ToList();

                    if (nonPgMailings.Any())
                    {
                        return nonPgMailings.First().PrintJobToWeichertSLs.Count();
                    }
                    else
                    {
                        return 0;
                    }
            }

            var pgMailings = _context.PureGoldMailings
                .Where(m =>
                    m.ActualPrintDate >= startdate
                    && m.ActualPrintDate <= enddate)
                .Join(_context.AppObjectToTransactions,
                    m => m.AppObjectToTransactionId,
                    t => t.AppObjectToTransactionId,
                    (m, t) => t)
                .Join(_context.AppObjects.Where(predicate),
                    anonymous => anonymous.AppObjectId,
                    a => a.AppObjectId,
                    (anonymous, a) => anonymous).ToList();

            return pgMailings.Count();
        }
        public int GetPrintJobProjections(int type, DateTime startdate, DateTime enddate)
        {
            Expression<Func<AppObject, bool>> predicate = a => a.AppObjectName.Contains("ThankYou");
            switch (type)
            {
                case 10: // Thank You
                    predicate = a => a.AppObjectId == 181; //T_PG_ThankYou
                    break;
                case 11: // Surveys Per Dan Use 182 Only (Same Envelope and Stamp) 
                    predicate = a => a.AppObjectId == 182; //T_PG_FollowUp or T_PG_Survey(a.AppObjectId == 183)
                    break;
                case 12: // Anniversary
                    predicate = a => a.AppObjectName.Contains("Anniversary"); //T_PG_Anniversary1234 or T_PG_Renew_Anniversary1234
                    break;
                case 13: // Spring Newsletters
                    predicate = a => a.AppObjectName.Contains("SpringSummerNewsletter"); // T_PG_SpringSummerNewsletter12345 or T_PG_Renew_SpringSummerNewsletter12345
                    break;
                case 14: // Fall Newsletters
                    predicate = a => a.AppObjectName.Contains("FallWinterNewsletter"); // T_PG_FallWinterNewsletter12345 or T_PG_Renew_FallWinterNewsletter12345
                    break;
                case 15: // Surveys Non-Pure Gold
                    // Not Supported Right Now
                    //var nonPgMailings = _context.PrintJobs
                    //    .Include("PrintJobToWeichertSLs")                        
                    //    .Where(pj => pj.PrintJobTypeId == type
                    //        && pj.PrintJobStatusId == 2 // Exported
                    //        && pj.UpdDt >= startdate 
                    //        && pj.UpdDt <= enddate).ToList();

                    //return nonPgMailings.Count();        
                    return 0;
            }

            var pgMailings = _context.PureGoldMailings
                .Where(m =>
                    m.ActualPrintDate == null
                   && m.ScheduledPrintDate >= startdate
                   && m.ScheduledPrintDate <= enddate)
                .Join(_context.AppObjectToTransactions,
                    m => m.AppObjectToTransactionId,
                    t => t.AppObjectToTransactionId,
                    (m, t) => t)
                .Join(_context.AppObjects.Where(predicate),
                    anonymous => anonymous.AppObjectId,
                    a => a.AppObjectId,
                    (anonymous, a) => anonymous).ToList();

            return pgMailings.Count();
        }
    }
}
