using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyUserSettingConfiguration : EntityTypeConfiguration<SurveyUserSetting>
    {
        public SurveyUserSettingConfiguration()
        {
            ToTable("tbPGS_SurveyUserSetting");
            HasKey(t => t.SurveyUserSettingId);
            Property(t => t.SurveyUserSettingId).HasColumnName("SurveyUserSettingId").IsRequired();
            Property(t => t.SurveyId).HasColumnName("SurveyId").IsRequired();
            Property(t => t.SettingName).HasColumnName("SettingName").HasColumnType("varchar").HasMaxLength(500);
            Property(t => t.SettingValue).HasColumnName("SettingValue").HasColumnType("varchar").HasMaxLength(3000);

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
