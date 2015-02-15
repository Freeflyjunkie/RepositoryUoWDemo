using System.Collections.Generic;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.ApplicationServices.Interfaces
{
    public interface IQualityAssuranceService
    {
        IEnumerable<QualityAssuranceService.QualityAssuranceIssue> ListQualityAssuranceIssues();
        IEnumerable<Transaction> ListTransactionsByQualityAssuranceIssueType(QualityAssuranceType type);
    }
}
