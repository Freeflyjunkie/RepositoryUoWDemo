using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IPrintJobService
    {
        IEnumerable<Int32> GetAppObjectToTransactionIdsByPrintJobId(Int32 printJobId, Int32? status);        
        PrintJobTypeEnum GetPrintJobTypeEnum(String printType);
        PrintJob CreatePrintJob(PrintJobTypeEnum type, string description, DateTime startDate, DateTime endDate, Int32 personNumber);
        PrintJob CreateEmptyPrintJob(PrintJobTypeEnum type, string description, DateTime startDate, DateTime endDate, Int32 personNumber);        
        IEnumerable<PrintJobWithCount> GetPrintJobsByPrintJobIds(IEnumerable<Int32> printJobIds);
        IEnumerable<Int32> GetPrintJobIds();
        PrintJob GetCurrentPrintJob();
        PrintJob GetPrintJobById(int printJobId);
        IEnumerable<PrintJobType> GetPrintJobTypes();
        IEnumerable<PrintJobItemStatus> GetPrintJobItemStatuses();
        Int32 GetPrintJobToAppObjectToTransactionsCount(Int32 printJobId, PrintJobItemStatusEnum? status);
        Int32 GetPrintJobToWeichertSLCount(Int32 printJobId, PrintJobItemStatusEnum? status);        
        IEnumerable<PrintJobToWeichertSL> GetPrintJobToWeichertSLsByPrintJobToWeichertSLIds(IEnumerable<Int32> printJobToWeichertSLIds);
        IEnumerable<Int32> GetPrintJobToWeichertSLIds(Int32 printJobId, Int32? printJobItemStatus);
        IEnumerable<Int32> GetPrintJobAppObjectToTransactionIds(Int32 printJobId, Int32? printJobItemStatus);
        IEnumerable<PrintJobToAppObjectToTransaction> GetPrintJobAppObjectToTransactionsByIds(Int32 printJobId, IEnumerable<Int32> printJobToAppObjectToTransactionIds);
        IEnumerable<PrintJobToAppObjectToTransaction> GetPrintJobToAppObjectToTransactionsByAppObjectToTransactionIds
            (Int32? printJobId, IEnumerable<Int32> appObjectToTransactionIds);
        PrintJobToAppObjectToTransaction GetPrintJobAppObjectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId);                
        void ExcludeAppObectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId);
        void ExcludePrintJobToWeichertSL(Int32 printJobId, Int32 saleId);
        void IncludeAppObectToTransaction(Int32 printJobId, Int32 appObjectToTransactionId);
        void IncludePrintJobToWeichertSL(Int32 printJobId, Int32 saleId);
        IEnumerable<AppObjectToTransaction> GetMissingCustomers(Int32 printJobId);        
        void UpdatePrintJobStatus(Int32 printJobId, Int32 personNumber, PrintJobStatusEnum status);
        void UpdatePrintJobAppObjectToTransactions(Int32 printJobId, Int32 appObjectToTransactionId);
        void UpdatePrintJobDescription(Int32 printJobId, String description);
        IEnumerable<Mailing> GetMailings(int transactionId);
        PureGoldMailing GetPureGoldMailing (int appObjectToTransactionId);
        void UpdateMailing(int mailingId);        
        PureGoldMailing GetMailing(Int32 appObjectToTransactionId);
        int GetPrintJobActuals(PrintJobTypeEnum type, DateTime startdate, DateTime enddate);
        int GetPrintJobProjections(PrintJobTypeEnum type, DateTime startdate, DateTime enddate);        
        IEnumerable<PrintExportNewsletter> GetExportedDataForExcel();
        IEnumerable<PrintExport> GetExportedDataForWord();
        void ExportPrintJob(Int32 printJobId);
    }
}
