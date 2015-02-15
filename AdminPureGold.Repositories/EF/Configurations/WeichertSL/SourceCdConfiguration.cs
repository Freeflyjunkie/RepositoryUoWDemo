using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class SourceCdConfiguration : EntityTypeConfiguration<SourceCd>
    {
        public SourceCdConfiguration()
        {
            ToTable("tbSourceCD");
            HasKey(t => t.SourceCode);
            Property(t => t.SourceCode).HasColumnName("SourceCD").IsRequired();
            Property(t => t.SourceDescription)
                .HasColumnName("SourceDesc")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();                        
        }
    }
}
