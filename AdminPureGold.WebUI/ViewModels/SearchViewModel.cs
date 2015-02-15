using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class SearchViewModel
    {
        public Transaction Transaction { get; set; }        
        public Property Property { get; set; }
        public Int32 SaleId { get; set; }
        public PropertyAlternate PropertyAlternate { get; set; }
        public IEnumerable<AgentViewModel> AgentViewModels { get; set; }        
    }
}