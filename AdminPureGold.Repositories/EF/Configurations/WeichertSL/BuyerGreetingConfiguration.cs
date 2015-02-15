using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class BuyerGreetingConfiguration : EntityTypeConfiguration<BuyerGreeting>
    {
        public BuyerGreetingConfiguration()
        {
            ToTable("tbBuyerGreeting");            
            HasKey(g => g.SaleId);
            Property(g => g.SaleId).HasColumnName("SaleID").IsRequired();
            Property(g => g.Title).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(g => g.GroupName).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(g => g.Salutation).HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();            
        }
    }
}
