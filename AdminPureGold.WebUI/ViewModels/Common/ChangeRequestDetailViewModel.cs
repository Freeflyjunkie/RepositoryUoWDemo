using AdminPureGold.ApplicationServices.DTO;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.WebUI.ViewModels.Common
{
    public class ChangeRequestDetailViewModel
    {
        public ChangeRequest ChangeRequest { get; set; }
        public ChangeRequestDetailParsed ChangeRequestDetailParsed { get; set; }
    }
}