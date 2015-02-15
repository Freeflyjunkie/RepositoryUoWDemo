using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyAnswerTextConfiguration : EntityTypeConfiguration<SurveyAnswerText>
    {
        public SurveyAnswerTextConfiguration()
        {
            ToTable("tbPGS_SurveyAnswerText");
            HasKey(t => t.SurveyAnswerTextId);
            Property(t => t.SurveyAnswerTextId).HasColumnName("SurveyAnswerTextId").IsRequired();
            Property(t => t.SurveyAnswerId).HasColumnName("SurveyAnswerId").IsRequired();
            Property(t => t.SurveyAnswerUserText).HasColumnName("SurveyAnswerText").HasColumnType("varchar").HasMaxLength(3000);

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
