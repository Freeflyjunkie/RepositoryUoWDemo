using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.Interfaces.Mrc
{
    public interface IMrcSqlQueryRepository<T> : IGenericSqlQueryRepository<T> where T : class
    {
        IEnumerable<T> GetMissingCustomers(Int32 printJobId);
        IEnumerable<T> GetMailingSchedule(int transactionId);
        void SetMailing(int mailingId);
        T CreatePrintJob(int printJobTypeId, string printJobDescription, DateTime startDate, DateTime endDate, int crtBy);
        Int32? CreateTransactionFromListId(Int32 listId);
        void ExportPrintJob(Int32 printJobId);
        IEnumerable<T> GetSurveyReportDetail(int choiceId, DateTime startDate, DateTime endDate);

    }
}
