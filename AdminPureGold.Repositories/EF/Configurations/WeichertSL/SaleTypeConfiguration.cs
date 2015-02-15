using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class SaleTypeConfiguration : EntityTypeConfiguration<SaleType>
    {
        public SaleTypeConfiguration()
        {
            ToTable("tbSaleType");
            HasKey(t => t.SaleTypeId);
            Property(t => t.SaleTypeId).HasColumnName("SaleTypeID").IsRequired();
            Property(t => t.SaleTypeDescription)
                .HasColumnName("SaleTypeDesc")
                .HasColumnType("varchar")
                .HasMaxLength(15)
                .IsRequired();                       
        }
    }
}
