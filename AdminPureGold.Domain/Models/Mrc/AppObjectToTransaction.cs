using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class AppObjectToTransaction : IModelWithState
    {
        public Int32 AppObjectToTransactionId { get; set; }
        public Int32 AppObjectId { get; set; }
        public Int32 TransactionId { get; set; }
        public Int32 CrtBy { get; set; }
        public DateTime CrtDt { get; set; }
        public Int32? UpdBy { get; set; }
        public DateTime? UpdDt { get; set; }
        public virtual ICollection<PureGoldMailing> PureGoldMailings { get; set; }
        public virtual AppObject AppObject { get; set; }
        public virtual ICollection<TransactionTemplate> TransactionTemplates { get; set; }
        public virtual Transaction Transaction { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
