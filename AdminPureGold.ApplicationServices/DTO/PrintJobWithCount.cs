using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.ApplicationServices.DTO
{
    public class PrintJobWithCount
    {
        public PrintJob PrintJob { get; set; }
        public Int32 IncludedPrintJobToAppObjectToTransactionCount { get; set; }
        public Int32 ExcludedPrintJobToAppObjectToTransactionCount { get; set; }
        public Int32 IncludedPrintJobToWeichertSLCount { get; set; }
        public Int32 ExcludedPrintJobToWeichertSLCount { get; set; }
    }
}
