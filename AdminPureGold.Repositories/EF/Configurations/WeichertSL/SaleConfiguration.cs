using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    internal class SaleConfiguration : EntityTypeConfiguration<Sale>
    {
        public SaleConfiguration()
        {
            ToTable("tbSale");
            HasKey(p => p.SaleId);
            Property(p => p.SaleId).HasColumnName("SaleID").IsRequired();

            Property(p => p.ListId).IsRequired();
            Property(p => p.SaleSequenceNumber).HasColumnName("SaleSeqNo").IsRequired();
            Property(p => p.OfficeId).HasColumnName("OfficeID").IsRequired();
            Property(p => p.SaleTypeId).HasColumnName("SaleTypeID").IsOptional();
            Property(p => p.SaleSourceCode)
                .HasColumnName("SaleSourceCD")
                .HasColumnType("varchar")
                .HasMaxLength(5)
                .IsOptional();
            Property(p => p.SaleStatus).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(p => p.SaleEntryDate).IsOptional();
            Property(p => p.SaleContractDate).IsOptional();
            Property(p => p.ScheduleCloseDate).IsOptional();
            Property(p => p.SalePrice).IsOptional();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();

            HasRequired(g => g.List).WithMany().HasForeignKey(g => g.ListId);

            HasMany(g => g.SaleToAssociates).WithRequired().HasForeignKey(g => g.SaleId);
            HasMany(g => g.SaleToBrokers).WithRequired().HasForeignKey(g => g.SaleId);
            HasMany(g => g.SaleToBuyers).WithRequired().HasForeignKey(g => g.SaleId);

            HasOptional(g => g.BuyerGreeting).WithRequired();
            HasOptional(g => g.Closing).WithRequired();

            HasOptional(g => g.SaleType).WithMany();
            HasOptional(cd => cd.SourceCd).WithMany().HasForeignKey(s => s.SaleSourceCode);
        }
    }
}
