using System;
using AdminPureGold.Domain.Interfaces;

namespace AdminPureGold.Domain.Models.Mrc
{
    public class SurveyUserSetting : IModelWithState
    {
        public Int32 SurveyUserSettingId { get; set; }
        public Int32 SurveyId { get; set; }
        public String SettingName { get; set; }
        public String SettingValue { get; set; }
        public State EntityStateForGraphsUpdates { get; set; }
    }
}
