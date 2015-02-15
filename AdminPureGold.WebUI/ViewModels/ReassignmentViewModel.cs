using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class ReassignmentViewModel
    {
        public Transaction Transaction { get; set; }
        public Property Property { get; set; }
        public PropertyAlternate PropertyAlternate { get; set; }
        public IEnumerable<AgentViewModel> AgentViewModels { get; set; } 
    }
}