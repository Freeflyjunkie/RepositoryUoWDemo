using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class PureGoldSaleConfiguration : EntityTypeConfiguration<PureGoldSale>
    {
        public PureGoldSaleConfiguration()
        {
            ToTable("tbPureGoldSale");
            HasKey(p => p.SaleToAssociateId);
            Property(p => p.SaleToAssociateId).HasColumnName("SaleToAssociateID").IsRequired();
            Property(p => p.PureGold).HasColumnType("char").HasMaxLength(1).IsOptional();
            Property(p => p.PgoMissionFlag).HasColumnName("PGOmissionFlag").IsRequired();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();
        }
    }
}
