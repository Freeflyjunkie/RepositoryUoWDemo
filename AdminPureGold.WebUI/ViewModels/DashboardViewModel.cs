using AdminPureGold.Domain.Models.Mrc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminPureGold.WebUI.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}