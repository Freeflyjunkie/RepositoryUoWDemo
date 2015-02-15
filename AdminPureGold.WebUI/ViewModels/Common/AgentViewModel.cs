using System;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.WebUI.ViewModels.Common
{
    public class AgentViewModel
    {
        public Int32 PersonNumber { get; set; }
        public RelateToName RelateToName { get; set; }
        public RelateToPhone RelateToPhone { get; set; }
        public RelateToEmail RelateToEmail { get; set; }
        public RelateToAddress RelateToAddress { get; set; }
        public Office Office { get; set; }
        public PersonToRelate OfficeSalesManager { get; set; }
        public PersonToImage PersonToImage { get; set; }
        public PersonToRelate ProcessingManager { get; set; }     
    }
}