using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface IPrintJobRepository : IGenericRepository<PrintJob>
    {
        IEnumerable<Int32> GetAppObjectToTransactionIdsByPrintJobId(Int32 printJobId, Int32? status);               
        IEnumerable<PrintJobToWeichertSL> GetPrintJobToWeichertSLsByPrintJobToWeichertSLIds(IEnumerable<Int32> printJobToWeichertSLIds);
        IEnumerable<Int32> GetPrintJobToWeichertSLIds(int printJobId, Int32? printJobItemStatus);
        PrintJobToAppObjectToTransaction GetPrintJobToAppObjectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId);
        IEnumerable<Int32> GetPrintJobAppObjectToTransactionIds(Int32 printJobId, Int32? printJobItemStatus);
        IEnumerable<PrintJobToAppObjectToTransaction> GetPrintJobToAppObjectToTransactionsByAppObjectToTransactionIds(Int32? printJobId, IEnumerable<Int32> appObjectToTransactionIds);        
        IEnumerable<PrintJobType> GetPrintJobTypes();
        IEnumerable<PrintJobItemStatus> GetPrintJobItemStatuses();
        Int32 GetPrintJobToAppObjectToTransactionsCount(Int32 printJobId, Int32? status);
        Int32 GetPrintJobToWeichertSLCount(Int32 printJobId, Int32? status);
        int GetPrintJobActuals(Int32 type, DateTime startdate, DateTime enddate);
        int GetPrintJobProjections(Int32 type, DateTime startdate, DateTime enddate);
    }
}
