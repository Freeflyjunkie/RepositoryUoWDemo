using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class ClosingConfiguration :EntityTypeConfiguration<Closing>
    {
        public ClosingConfiguration()
        {
            ToTable("tbClosing");            
            HasKey(g => g.SaleId);
            Property(g => g.SaleId).HasColumnName("SaleID").IsRequired();
            Property(g => g.FinalSalePrice).IsRequired();
            Property(g => g.ActualCloseDate).IsRequired();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();
        }
    }
}
