using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class SaleToBuyerConfiguration : EntityTypeConfiguration<SaleToBuyer>
    {
        public SaleToBuyerConfiguration()
        {
            ToTable("tbSaleToBuyer");
            HasKey(p => p.SaleToBuyerId);
            Property(p => p.SaleToBuyerId).HasColumnName("SaleToBuyerID").IsRequired();
            Property(p => p.SaleId).HasColumnName("SaleID").IsRequired();
            Property(p => p.BuyerSequenceNumber).HasColumnName("BuyerSeqNo").IsRequired();
            Property(p => p.Title).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.FirstName).HasColumnName("FName").HasColumnType("varchar").HasMaxLength(35).IsOptional();
            Property(p => p.MiddleInitial).HasColumnName("MInit").HasColumnType("varchar").HasMaxLength(1).IsOptional();
            Property(p => p.LastName).HasColumnName("LName").HasColumnType("varchar").HasMaxLength(35).IsOptional();
            Property(p => p.Suffix).HasColumnType("varchar").HasMaxLength(5).IsOptional();
            Property(p => p.Address1).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.Address2).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.City).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.State).HasColumnType("varchar").HasMaxLength(2).IsOptional();
            Property(p => p.Zip).HasColumnType("varchar").HasMaxLength(15).IsOptional();
            Property(p => p.Country).HasColumnType("varchar").HasMaxLength(2).IsOptional();
            Property(p => p.HomePhone).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.BusinessPhone).HasColumnName("BusPhone").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.Email).HasColumnType("varchar").HasMaxLength(75).IsOptional();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();         
        }
    }
}
