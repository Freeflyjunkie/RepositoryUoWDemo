
using System;
using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO.Bing;
using AdminPureGold.ApplicationServices.Enums;
using AdminPureGold.ApplicationServices.Services;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class QualityAssuranceIndexViewModel
    {
        public IEnumerable<QualityAssuranceService.QualityAssuranceIssue> QualityAssuranceIssues { get; set; }
        public QualityAssuranceType IssueType { get; set; }
        public int PropertyAlternatesPage { get; set; }
        public IEnumerable<PropertyAlternate> PropertyAlternates { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public IEnumerable<AgentViewModel> AgentViewModels { get; set; }
        public Location BingLocation { get; set; }
        public IEnumerable<List> WeichertSlLists { get; set; }
        public Int32 Total { get; set; }
        public Int32 PageNumber { get; set; }
        public Int32 PageCount { get; set; }
        public Boolean HasPreviousPage { get; set; }
        public Boolean HasNextPage { get; set; }
    }
}
