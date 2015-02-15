using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;
using System;

namespace AdminPureGold.WebUI.ViewModels
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }        
        public Property Property { get; set; }
        public PropertyAlternate PropertyAlternate { get; set; }
        public IEnumerable<Mailing> Mailings { get; set; }
        public PrintJob CurrentPrintJob { get; set; }
        public IEnumerable<PrintJobToAppObjectToTransaction> PrintJobToAppObjectToTransactions { get; set; }
        public List List { get; set; }
        public IEnumerable<AgentViewModel> AgentViewModels { get; set; }
        public IEnumerable<ChangeRequestDetailParsed> ChangeRequestDetailParsed { get; set; }
    }
}
