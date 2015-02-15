using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class RelateToAddressConfiguration : EntityTypeConfiguration<RelateToAddress>
    {
        public RelateToAddressConfiguration()
        {
            ToTable("tbRelateToAddress");
            HasKey(t => t.RelateToAddressId);
            Property(t => t.RelateToAddressId).IsRequired();

            Property(t => t.RelationshipNumber).HasColumnName("WRELATENO").IsOptional();
            Property(t => t.AddressRoleCode)
                .HasColumnName("AddressRoleCD")
                .HasColumnType("varchar")
                .HasMaxLength(8)
                .IsOptional();
            Property(t => t.Address1).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(t => t.Address2).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.City).HasColumnType("varchar").HasMaxLength(75).IsRequired();
            Property(t => t.State).HasColumnType("varchar").HasMaxLength(2).IsRequired();
            Property(t => t.Zip).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            Property(t => t.ZipExt).HasColumnType("varchar").HasMaxLength(4).IsOptional();
            Property(t => t.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.CrDate).HasColumnName("CRDATE").HasColumnType("datetime").IsOptional();
            Property(t => t.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.ChDate).HasColumnName("CHDATE").HasColumnType("datetime").IsOptional();
        }
    }
}
