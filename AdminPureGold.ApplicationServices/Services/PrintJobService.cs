using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Repositories.Interfaces.Mrc;
using AdminPureGold.Repositories.Repositories.Mrc;

namespace AdminPureGold.ApplicationServices.Services
{
    public class PrintJobService : IPrintJobService
    {
        private readonly IUnitOfWorkMrc _unitOfWorkMrc;

        public PrintJobService(IUnitOfWorkMrc unitOfWorkMrc)
        {
            _unitOfWorkMrc = unitOfWorkMrc;           
        }

        public IEnumerable<Int32> GetAppObjectToTransactionIdsByPrintJobId(Int32 printJobId, Int32? status)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetAppObjectToTransactionIdsByPrintJobId(printJobId, status).ToList();
        }
        public PrintJobTypeEnum GetPrintJobTypeEnum(String printType)
        {
            if (printType.Contains("Thank You"))
                return PrintJobTypeEnum.ThankYouCards;

            if (printType.Contains("Follow Up") || printType.Contains("Survey"))
                return PrintJobTypeEnum.SurveyAndFollowUps;

            if (printType.Contains("Anniversary"))
                return PrintJobTypeEnum.AnniversaryCards;

            if (printType.Contains("Fall"))
                return PrintJobTypeEnum.FallNewsletters;

            if (printType.Contains("Spring"))
                return PrintJobTypeEnum.SpringNewletters;

            return PrintJobTypeEnum.SurveyAndFollowUpsNonPg;
        }
        public PrintJob CreatePrintJob(PrintJobTypeEnum type, string description, DateTime startDate, DateTime endDate, Int32 personNumber)
        {
            description = Regex.Replace(description, @"[!@#$%'_]", "");
            var mrcSqlQueryRepository = new MrcSqlQueryRepository<PrintJob>();
            var printJob = mrcSqlQueryRepository.CreatePrintJob(Convert.ToInt32(type), description, startDate, endDate, personNumber);
            printJob.PrintJobTypeId = Convert.ToInt32(type);
            return printJob;            
        }
        public PrintJob CreateEmptyPrintJob(PrintJobTypeEnum type, string description, DateTime startDate, DateTime endDate, Int32 personNumber)
        {
            description = Regex.Replace(description, @"[!@#$%'_]", "");
            var newPrintJob = new PrintJob
            {                
                PrintJobDesc = description,
                PrintJobTypeId = (Int32)type,
                PrintJobStatusId = 1,
                StartDate = startDate,
                EndDate = endDate,
                CrtBy = personNumber,
                CrtDt = DateTime.Now,
                EntityStateForGraphsUpdates = State.Added
            };

            _unitOfWorkMrc.PrintJobRepository.Insert(newPrintJob);
            _unitOfWorkMrc.Save();

            return newPrintJob;
        }        
        public IEnumerable<PrintJobWithCount> GetPrintJobsByPrintJobIds(IEnumerable<Int32> printJobIds)
        {
            var printJobs = _unitOfWorkMrc.PrintJobRepository.Get(pj => printJobIds.Contains(pj.PrintJobId),
                includeProperties: "PrintJobStatus")
                .OrderByDescending(p => p.CrtDt)
                .ToList();

            var printJobWithCounts = new List<PrintJobWithCount>();
            foreach (var printJob in printJobs)
            {
                var printJobWithCount = new PrintJobWithCount
                {
                    PrintJob = printJob,
                    IncludedPrintJobToAppObjectToTransactionCount = GetPrintJobToAppObjectToTransactionsCount(printJob.PrintJobId, PrintJobItemStatusEnum.Include),
                    ExcludedPrintJobToAppObjectToTransactionCount = GetPrintJobToAppObjectToTransactionsCount(printJob.PrintJobId, PrintJobItemStatusEnum.Exclude),
                    IncludedPrintJobToWeichertSLCount = GetPrintJobToWeichertSLCount(printJob.PrintJobId, PrintJobItemStatusEnum.Include),
                    ExcludedPrintJobToWeichertSLCount = GetPrintJobToWeichertSLCount(printJob.PrintJobId, PrintJobItemStatusEnum.Exclude)
                };

                printJobWithCounts.Add(printJobWithCount);
            }
            return printJobWithCounts;
        }
        public IEnumerable<Int32> GetPrintJobIds()
        {
            return _unitOfWorkMrc.PrintJobRepository
                .Get(orderBy: p => p.OrderByDescending(pj => pj.CrtDt))
                .Select(pj => pj.PrintJobId)
                .ToList();
        }
        public PrintJob GetCurrentPrintJob()
        {
            return _unitOfWorkMrc.PrintJobRepository.Get(pj => pj.PrintJobStatusId == 1).ToList().FirstOrDefault();                     
        }
        public PrintJob GetPrintJobById(int printJobId)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
        }
        public IEnumerable<PrintJobType> GetPrintJobTypes()
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobTypes().ToList();
        }
        public IEnumerable<PrintJobItemStatus> GetPrintJobItemStatuses()
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobItemStatuses().ToList();
        }
        public Int32 GetPrintJobToAppObjectToTransactionsCount(Int32 printJobId, PrintJobItemStatusEnum? status)
        {            
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobToAppObjectToTransactionsCount(printJobId, (Int32?)status);
        }
        public Int32 GetPrintJobToWeichertSLCount(Int32 printJobId, PrintJobItemStatusEnum? status)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobToWeichertSLCount(printJobId, (Int32?)status);
        }       
        public IEnumerable<PrintJobToWeichertSL> GetPrintJobToWeichertSLsByPrintJobToWeichertSLIds(IEnumerable<Int32> printJobToWeichertSLIds)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobToWeichertSLsByPrintJobToWeichertSLIds(printJobToWeichertSLIds).ToList();
        }
        public IEnumerable<Int32> GetPrintJobToWeichertSLIds(Int32 printJobId, Int32? printJobItemStatus)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobToWeichertSLIds(printJobId, printJobItemStatus).ToList();
        }
        public IEnumerable<Int32> GetPrintJobAppObjectToTransactionIds(Int32 printJobId, Int32? printJobItemStatus)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobAppObjectToTransactionIds(printJobId, printJobItemStatus).ToList();
        }
        public IEnumerable<PrintJobToAppObjectToTransaction> GetPrintJobAppObjectToTransactionsByIds(Int32 printJobId, IEnumerable<Int32> printJobToAppObjectToTransactionIds)
        {
            var printJobToAppObjectToTransactions = _unitOfWorkMrc.PrintJobToAppObjectToTransactionRepository
                .Get(pta => pta.PrintJobId == printJobId && 
                    printJobToAppObjectToTransactionIds.Contains(pta.PrintJobToAppObjectToTransactionId), 
                includeProperties: 
                "PrintJobItemStatus," +
                "AppObjectToTransaction," +
                "AppObjectToTransaction.PureGoldMailings," +
                "AppObjectToTransaction.Transaction").ToList();
            return printJobToAppObjectToTransactions;
        }
        public IEnumerable<PrintJobToAppObjectToTransaction> GetPrintJobToAppObjectToTransactionsByAppObjectToTransactionIds
            (Int32? printJobId, IEnumerable<Int32> appObjectToTransactionIds)
        {
            return
                _unitOfWorkMrc.PrintJobRepository.GetPrintJobToAppObjectToTransactionsByAppObjectToTransactionIds(
                    printJobId, appObjectToTransactionIds);
        }
        public PrintJobToAppObjectToTransaction GetPrintJobAppObjectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobToAppObjectToTransaction(printJobId, appObjectToTransactionId);
        }        
        public void ExcludeAppObectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
            if (printJob != null)
            {
                var printJobItem = printJob.PrintJobToAppObjectToTransactions.SingleOrDefault(
                    t => t.AppObjectToTransactionId == appObjectToTransactionId);

                if (printJobItem != null)
                {
                    printJobItem.PrintJobItemStatusId = 2;
                    printJobItem.EntityStateForGraphsUpdates = State.Modified;
                    _unitOfWorkMrc.PrintJobRepository.Update(printJob);
                }
                _unitOfWorkMrc.Save();
            }            
        }
        public void ExcludePrintJobToWeichertSL(Int32 printJobId, Int32 saleId)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
            if (printJob != null)
            {
                var printJobItem = printJob.PrintJobToWeichertSLs.SingleOrDefault(                    
                    t => t.SaleId == saleId);

                if (printJobItem != null)
                {
                    printJobItem.PrintJobItemStatusId = 2;
                    printJobItem.EntityStateForGraphsUpdates = State.Modified;
                    _unitOfWorkMrc.PrintJobRepository.Update(printJob);
                }
                _unitOfWorkMrc.Save();
            }            
        }
        public void IncludeAppObectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
            if (printJob != null)
            {
                var printJobItem =
                        printJob.PrintJobToAppObjectToTransactions.SingleOrDefault(
                            item => item.AppObjectToTransactionId == appObjectToTransactionId);
                
                if (printJobItem == null)                
                {
                    printJobItem = new PrintJobToAppObjectToTransaction
                    {
                        PrintJobId = printJobId,
                        PrintJobItemStatusId = 1,
                        AppObjectToTransactionId = appObjectToTransactionId,
                        EntityStateForGraphsUpdates = State.Added
                    };
                    printJob.PrintJobToAppObjectToTransactions.Add(printJobItem);
                }
                else
                {
                    printJobItem.PrintJobItemStatusId = 1;
                    printJobItem.EntityStateForGraphsUpdates = State.Modified;
                }
                
                _unitOfWorkMrc.PrintJobRepository.Update(printJob);
                _unitOfWorkMrc.Save();
            }            
        }
        public void IncludePrintJobToWeichertSL(Int32 printJobId, Int32 saleId)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
            if (printJob != null)
            {
                var printJobItem =
                        printJob.PrintJobToWeichertSLs.SingleOrDefault(
                            item => item.SaleId == saleId);

                if (printJobItem == null)
                {
                    printJobItem = new PrintJobToWeichertSL
                    {
                        PrintJobId = printJobId,
                        PrintJobItemStatusId = 1,
                        SaleId = saleId,
                        EntityStateForGraphsUpdates = State.Added
                    };
                    printJob.PrintJobToWeichertSLs.Add(printJobItem);
                }
                else
                {
                    printJobItem.PrintJobItemStatusId = 1;
                    printJobItem.EntityStateForGraphsUpdates = State.Modified;
                }

                _unitOfWorkMrc.PrintJobRepository.Update(printJob);
                _unitOfWorkMrc.Save();
            }   
        }
        public IEnumerable<AppObjectToTransaction> GetMissingCustomers(Int32 printJobId)
        {
            var mrcSqlQueryRepository = new MrcSqlQueryRepository<AppObjectToTransaction>();
            return mrcSqlQueryRepository.GetMissingCustomers(printJobId);
        }        
        public void UpdatePrintJobStatus(Int32 printJobId, Int32 personNumber, PrintJobStatusEnum status)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);                

            if (printJob != null)
            {
                var currentPrintJob = GetCurrentPrintJob();

                printJob.PrintJobStatusId = (Int32) status;
                printJob.UpdBy = personNumber;
                printJob.UpdDt = DateTime.Now;
                printJob.EntityStateForGraphsUpdates = State.Modified;
                _unitOfWorkMrc.PrintJobRepository.Update(printJob);

                if (currentPrintJob != null)
                {
                    if (currentPrintJob.PrintJobId != printJob.PrintJobId && status == PrintJobStatusEnum.Unexported)
                    {
                        currentPrintJob.PrintJobStatusId = (Int32) PrintJobStatusEnum.Cancelled;
                        currentPrintJob.UpdBy = personNumber;
                        currentPrintJob.UpdDt = DateTime.Now;
                        currentPrintJob.EntityStateForGraphsUpdates = State.Modified;
                        _unitOfWorkMrc.PrintJobRepository.Update(currentPrintJob);
                    }
                }

                if (status == PrintJobStatusEnum.MarkedAsSuccessful)
                {
                    var appObjectToTransactionIds = GetAppObjectToTransactionIdsByPrintJobId(printJobId, 1);
                    var pureGoldMailings = _unitOfWorkMrc.PureGoldMailingRepository
                        .Get(t => appObjectToTransactionIds.Contains(t.AppObjectToTransactionId));

                    foreach (var pureGoldMailing in pureGoldMailings)
                    {
                        pureGoldMailing.ActualPrintDate = DateTime.Now;
                        pureGoldMailing.EntityStateForGraphsUpdates = State.Modified;
                        _unitOfWorkMrc.PureGoldMailingRepository.Update(pureGoldMailing);
                    }
                }

                _unitOfWorkMrc.Save();
            }
        }
        public void UpdatePrintJobDescription(Int32 printJobId, String description)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
            printJob.PrintJobDesc = description;
            printJob.UpdBy = 22200293;
            printJob.UpdDt = DateTime.Now;
            printJob.EntityStateForGraphsUpdates = State.Modified;
            _unitOfWorkMrc.PrintJobRepository.Update(printJob);
            _unitOfWorkMrc.Save();
        }
        public IEnumerable<Mailing> GetMailings(int transactionId)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<Mailing>();
            var result = mrcCoreSqlQueryRepository.GetMailingSchedule(transactionId);
            return result;
        }
        public void UpdateMailing(int mailingId)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<Mailing>();
            mrcCoreSqlQueryRepository.SetMailing(mailingId);
        }        
        public PureGoldMailing GetMailing(Int32 appObjectToTransactionId)
        {
            return
                _unitOfWorkMrc.PureGoldMailingRepository.Get(t => t.AppObjectToTransactionId == appObjectToTransactionId)
                    .FirstOrDefault();
        }
        public PureGoldMailing GetPureGoldMailing(int appObjectToTransactionId)
        {
            return
                _unitOfWorkMrc.PureGoldMailingRepository.Get(t => t.AppObjectToTransactionId == appObjectToTransactionId).ToList().FirstOrDefault();
        }

        public void UpdatePrintJobAppObjectToTransactions(Int32 printJobId, Int32 appObjectToTransactionId)
        {
            var printJob = _unitOfWorkMrc.PrintJobRepository.GetById(printJobId);
            printJob.EntityStateForGraphsUpdates = State.Unchanged;

            printJob.PrintJobToAppObjectToTransactions = 
                printJob.PrintJobToAppObjectToTransactions ?? Enumerable.Empty<PrintJobToAppObjectToTransaction>().ToList();

            if (printJob.PrintJobToAppObjectToTransactions.All(pji => pji.AppObjectToTransactionId != appObjectToTransactionId))
            {
                printJob.PrintJobToAppObjectToTransactions.Add(new PrintJobToAppObjectToTransaction
                {
                    PrintJobId = printJobId,
                    AppObjectToTransactionId = appObjectToTransactionId,
                    PrintJobItemStatusId = 1,
                    EntityStateForGraphsUpdates = State.Added
                });

                _unitOfWorkMrc.PrintJobRepository.Update(printJob);
                _unitOfWorkMrc.Save();
            }
        }
        public int GetPrintJobActuals(PrintJobTypeEnum type, DateTime startdate, DateTime enddate)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobActuals((Int32) type, startdate, enddate);
        }
        public int GetPrintJobProjections(PrintJobTypeEnum type, DateTime startdate, DateTime enddate)
        {
            return _unitOfWorkMrc.PrintJobRepository.GetPrintJobProjections((Int32)type, startdate, enddate);
        }        
        public IEnumerable<PrintExportNewsletter> GetExportedDataForExcel()
        {
            return _unitOfWorkMrc.PrintExportNewsletterRepository.Get().ToList();
        }
        public IEnumerable<PrintExport> GetExportedDataForWord()
        {
            return _unitOfWorkMrc.PrintExportRepository.Get().ToList();
        }
        public void ExportPrintJob(Int32 printJobId)
        {
            var mrcCoreSqlQueryRepository = new MrcSqlQueryRepository<PrintJob>();
            mrcCoreSqlQueryRepository.ExportPrintJob(printJobId);
        }
    }
}
