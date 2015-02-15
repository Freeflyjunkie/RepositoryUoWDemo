using AdminPureGold.Domain.Models.AtlasX;
using System.Data.Entity.ModelConfiguration;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class PropertyAlternateConfiguration : EntityTypeConfiguration<PropertyAlternate>
    {
        public PropertyAlternateConfiguration()
        {
            ToTable("tbPropertyAlternate");            
            HasKey(p => p.PropertyAlternateId);
            Property(p => p.PropertyAlternateId).HasColumnName("PropertyAlternateID").IsRequired();
            Property(p => p.PropertyId).HasColumnName("PropertyID").IsRequired();
            Property(p => p.PersonNumber).HasColumnName("WPersno").IsRequired();
            Property(p => p.AltAddress1).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(p => p.AltAddress2).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(p => p.AltCity).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(p => p.AltState).HasColumnType("varchar").HasMaxLength(2).IsRequired();
            Property(p => p.AltZip).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            Property(p => p.AltZip4).HasColumnType("varchar").HasMaxLength(4).IsOptional();
            Property(p => p.AltBlock).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.AltLot).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.CrUser).HasColumnName("CRuser").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(p => p.CrDate).HasColumnName("CRdate").IsOptional();
            Property(p => p.ChUser).HasColumnName("CHuser").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.ChDate).HasColumnName("CHdate").IsOptional();

            Ignore(p => p.EntityStateForGraphsUpdates);
        }
    }
}
