using System.Collections.Generic;
using System.Linq;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Interfaces;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.Interfaces.Mrc;

namespace AdminPureGold.ApplicationServices.Services
{
    public class QualityAssuranceService : IQualityAssuranceService
    {
        private readonly IUnitOfWorkMrc _unitOfWorkMrc;

        public QualityAssuranceService(IUnitOfWorkMrc unitOfWorkMrc)
        {
            _unitOfWorkMrc = unitOfWorkMrc;
        }

        public struct QualityAssuranceIssue
        {
            public QualityAssuranceType Type;
            public int Count;
            public string ShortDescription;
            public string Description;
        }
        public IEnumerable<QualityAssuranceIssue> ListQualityAssuranceIssues()
        {
            var qaIssues = new List<QualityAssuranceIssue>
            {
                new QualityAssuranceIssue
                {
                    Type = QualityAssuranceType.InvalidAddress,
                    Count = _unitOfWorkMrc.TransactionRepository.ListInvalidAddressTransactions().Count(),
                    ShortDescription = "Invalid Addresses",
                    Description = "With Invalid Addresses"
                },
                new QualityAssuranceIssue
                {
                    Type = QualityAssuranceType.MissingData,
                    Count = _unitOfWorkMrc.TransactionRepository.ListMissingDataTransactions().Count(),
                    ShortDescription = "Missing Customer",
                    Description = "With Missing Customer"
                },
                new QualityAssuranceIssue
                {
                    Type = QualityAssuranceType.NoOwnership,
                    Count = _unitOfWorkMrc.TransactionRepository.ListNoOwnershipTransactions().Count(),
                    ShortDescription = "No Ownership",
                    Description = "With No Ownership"
                }
            };

            var printJobs = _unitOfWorkMrc.PrintJobRepository.Get(pj => pj.PrintJobStatusId == 1);
            var enumerable = printJobs as IList<PrintJob> ?? printJobs.ToList();
            var currentPrintJob = enumerable.FirstOrDefault();
            if (currentPrintJob != null)
            {
                qaIssues.Add(new QualityAssuranceIssue
                {
                    Type = QualityAssuranceType.PrintJob,
                    Count =
                        _unitOfWorkMrc.TransactionRepository.ListPrintJobFailedQaTransactions(currentPrintJob.PrintJobId)
                            .Count(),
                    ShortDescription = "Current Print Job",
                    Description = "With Current Print Job"
                });
            }

            return qaIssues;
            //yield return new QualityAssuranceIssue
            //{
            //    Type = QualityAssuranceType.InvalidAddress,
            //    Count = _unitOfWorkMrc.TransactionRepository.ListInvalidAddressTransactions().Count(),
            //    ShortDescription = "Invalid Addresses",
            //    Description = "With Invalid Addresses"
            //};

            //yield return new QualityAssuranceIssue
            //{
            //    Type = QualityAssuranceType.MissingData,
            //    Count = _unitOfWorkMrc.TransactionRepository.ListMissingDataTransactions().Count(),
            //    ShortDescription = "Missing Customer",
            //    Description = "With Missing Customer"
            //};

            //yield return new QualityAssuranceIssue
            //{
            //    Type = QualityAssuranceType.NoOwnership,
            //    Count = _unitOfWorkMrc.TransactionRepository.ListNoOwnershipTransactions().Count(),
            //    ShortDescription = "No Ownership",
            //    Description = "With No Ownership"
            //};           
        }
        public IEnumerable<Transaction> ListTransactionsByQualityAssuranceIssueType(QualityAssuranceType type)
        {
            IEnumerable<Transaction> result = null;

            switch (type)
            {
                case QualityAssuranceType.InvalidAddress:
                    result = _unitOfWorkMrc.TransactionRepository.ListInvalidAddressTransactions();
                    break;

                case QualityAssuranceType.MissingData:
                    result = _unitOfWorkMrc.TransactionRepository.ListMissingDataTransactions();
                    break;

                case QualityAssuranceType.NoOwnership:
                    result = _unitOfWorkMrc.TransactionRepository.ListNoOwnershipTransactions();
                    break;

                case QualityAssuranceType.PrintJob:
                    var printJobs = _unitOfWorkMrc.PrintJobRepository.Get(pj => pj.PrintJobStatusId == 1);
                    var enumerable = printJobs as IList<PrintJob> ?? printJobs.ToList();
                    var currentPrintJob = enumerable.FirstOrDefault();
                    var printJobId = 0;

                    if (currentPrintJob != null)
                    {
                        printJobId = currentPrintJob.PrintJobId;
                    }

                    result = _unitOfWorkMrc.TransactionRepository.ListPrintJobFailedQaTransactions(printJobId);
                    break;
            }

            return result;
        }
    }
}
