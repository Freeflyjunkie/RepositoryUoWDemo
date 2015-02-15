using System.Collections.Generic;
using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.CorpComm;
using AdminPureGold.WebUI.ViewModels.Common;
using System;

namespace AdminPureGold.WebUI.ViewModels
{
    public class CorpCommViewModel
    {
        public IEnumerable<McMessage> McMessages { get; set; }
    }
}