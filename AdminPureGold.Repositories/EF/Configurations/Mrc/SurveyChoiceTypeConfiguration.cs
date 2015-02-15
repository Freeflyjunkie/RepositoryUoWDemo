using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyChoiceTypeConfiguration : EntityTypeConfiguration<SurveyChoiceType>
    {
        public SurveyChoiceTypeConfiguration()
        {
            ToTable("tbPGS_ChoiceType");
            HasKey(t => t.ChoiceTypeId);
            Property(t => t.ChoiceTypeId).HasColumnName("ChoiceTypeId").IsRequired();
            Property(t => t.ChoiceTypeDescription).HasColumnName("ChoiceTypeDescription").HasColumnType("varchar").HasMaxLength(50).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
