using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.WebUI.ViewModels.Common;
using System;

namespace AdminPureGold.WebUI.ViewModels
{
    public class EmailViewModel
    {
        public IEnumerable<PureGoldEmail> PureGoldEmail { get; set; }
        public PureGoldEmailSetting PureGoldEmailSettings { get; set; }
    }
}