using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyAnswerConfiguration : EntityTypeConfiguration<SurveyAnswer>
    {
        public SurveyAnswerConfiguration()
        {
            ToTable("tbPGS_SurveyAnswer");
            HasKey(t => t.SurveyAnswerId);
            Property(t => t.SurveyAnswerId).HasColumnName("SurveyAnswerId").IsRequired();
            Property(t => t.SurveyId).HasColumnName("SurveyId").IsRequired();
            Property(t => t.ChoiceId).HasColumnName("ChoiceId").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
