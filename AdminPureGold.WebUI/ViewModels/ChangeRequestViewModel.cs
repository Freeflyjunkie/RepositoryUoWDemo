using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class ChangeRequestViewModel
    {
        public IEnumerable<Domain.Models.Mrc.ChangeRequest> ChangeRequests { get; set; }
        public IEnumerable<ChangeRequestDetailParsed> ChangeRequestDetailParsed { get; set; }
        public IEnumerable<AgentViewModel> AgentViewModels { get; set; }
        public Int32 PageNumber { get; set; }
        public Int32 PageCount { get; set; }
        public Boolean HasPreviousPage { get; set; }
        public Boolean HasNextPage { get; set; }
    }
}