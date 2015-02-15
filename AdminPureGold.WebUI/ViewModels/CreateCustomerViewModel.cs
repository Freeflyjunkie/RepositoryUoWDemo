using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class CreateCustomerViewModel
    {
        public List List { get; set; }
        public Sale ClosedSale { get; set; }
        public IEnumerable<AgentViewModel> AgentViewModels { get; set; }
        public Boolean AllowCreate { get; set; }
    }
}