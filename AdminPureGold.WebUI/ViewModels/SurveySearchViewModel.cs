using System;
using System.Collections.Generic;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class SurveySearchViewModel
    {
        public List List { get; set; }
        public Sale ClosedSale { get; set; }
    }
}