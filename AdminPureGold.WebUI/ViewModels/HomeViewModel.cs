
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<QualityAssuranceService.QualityAssuranceIssue> QualityAssuranceIssues { get; set; }        
        public IEnumerable<Domain.Models.Mrc.ChangeRequest> ChangeRequests { get; set; }
        public IEnumerable<ChangeRequestDetailParsed> ChangeRequestDetailParsed { get; set; }
        //public IEnumerable<AgentViewModel> AgentViewModels { get; set; }
        public PrintJob CurrentPrintJob { get; set; }
    }
}
