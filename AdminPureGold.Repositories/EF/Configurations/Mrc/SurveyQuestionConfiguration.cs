using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyQuestionConfiguration : EntityTypeConfiguration<SurveyQuestion>
    {
        public SurveyQuestionConfiguration()
        {
            ToTable("tbPGS_Question");
            HasKey(t => t.QuestionId);
            Property(t => t.QuestionId).HasColumnName("QuestionId").IsRequired();
            Property(t => t.QuestionText).HasColumnName("QuestionText").HasColumnType("varchar").HasMaxLength(2000).IsRequired();
            Property(t => t.Active).HasColumnName("Active").IsRequired();
            Property(t => t.SortOrder).HasColumnName("SortOrder");

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
