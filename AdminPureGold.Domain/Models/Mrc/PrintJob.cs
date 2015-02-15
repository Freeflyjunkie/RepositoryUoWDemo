using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class PrintJob : IModelWithState
    {
        public Int32 PrintJobId { get; set; }
        public String PrintJobDesc { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Int32 PrintJobTypeId  { get; set; }        
        public Int32 PrintJobStatusId { get; set; }        
        public Int32 CrtBy { get; set; }
        public DateTime CrtDt { get; set; }
        public Int32? UpdBy { get; set; }
        public DateTime? UpdDt { get; set; }
        public virtual PrintJobType PrintJobType { get; set; }
        public virtual PrintJobStatus PrintJobStatus { get; set; }
        public virtual ICollection<PrintJobToAppObjectToTransaction> PrintJobToAppObjectToTransactions { get; set; }
        public virtual ICollection<PrintJobToWeichertSL> PrintJobToWeichertSLs { get; set; }

        public State EntityStateForGraphsUpdates { get; set; }
    }
}
