using AdminPureGold.Domain.Models.Mrc;
using AdminPureGold.WebUI.ViewModels.Common;

namespace AdminPureGold.WebUI.ViewModels
{
    public class ChangeRequestCommentHistory
    {
        public ChangeRequestComment ChangeRequestComment { get; set; }
        public AgentViewModel AgentViewModel { get; set; }
    }
}