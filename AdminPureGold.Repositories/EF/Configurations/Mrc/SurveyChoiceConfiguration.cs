using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyChoiceConfiguration : EntityTypeConfiguration<SurveyChoice>
    {
        public SurveyChoiceConfiguration()
        {
            ToTable("tbPGS_Choice");
            HasKey(t => t.ChoiceId);
            Property(t => t.ChoiceId).HasColumnName("ChoiceId").IsRequired();
            Property(t => t.QuestionId).HasColumnName("QuestionId").IsRequired();
            Property(t => t.ChoiceTypeId).HasColumnName("ChoiceTypeId").IsRequired();
            Property(t => t.ChoiceText).HasColumnName("ChoiceText").IsRequired();
            Property(t => t.SortOrder).HasColumnName("SortOrder");
            Property(t => t.Active).HasColumnName("Active");

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
