using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class SaleToAssociateConfiguration : EntityTypeConfiguration<SaleToAssociate>
    {
        public SaleToAssociateConfiguration()
        {
            ToTable("tbSaleToAssociate");            
            HasKey(g => g.SaleToAssociateId);
            Property(g => g.SaleToAssociateId).HasColumnName("SaleToAssociateID").IsRequired();
            Property(g => g.SaleId).HasColumnName("SaleID").IsRequired();
            Property(g => g.SaleAssociateNumber)
                .HasColumnName("SaleAssocNo")
                .HasColumnType("varchar")
                .HasMaxLength(5)
                .IsRequired();
            Property(g => g.AsscociateType)
                .HasColumnName("AssocType")
                .HasColumnType("varchar")
                .HasMaxLength(2)
                .IsRequired();
            Property(g => g.SaleAssociateOfficeId).HasColumnName("SaleAssocOfficeID").IsRequired();
            Property(g => g.PersonNumber).HasColumnName("WPersno").IsOptional();
            Property(g => g.RelationshipNumber).HasColumnName("WRelateNo").IsOptional();

            HasOptional(g => g.PureGoldSale).WithRequired();
        }
    }
}
