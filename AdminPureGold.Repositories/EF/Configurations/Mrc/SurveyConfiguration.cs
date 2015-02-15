using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class SurveyConfiguration : EntityTypeConfiguration<Survey>
    {
        public SurveyConfiguration()
        {            
            ToTable("tbPGS_Survey");
            HasKey(t => t.SurveyId);
            Property(t => t.SurveyId).HasColumnName("SurveyId").IsRequired();
            Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber").HasColumnType("char").HasMaxLength(9).IsOptional();
            Property(t => t.SaleId).HasColumnName("SaleId").IsOptional();
            Property(t => t.InputDate).HasColumnName("InputDate").HasColumnType("datetime").IsOptional();
            Property(t => t.InputBy).HasColumnName("InputBy").IsOptional();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
