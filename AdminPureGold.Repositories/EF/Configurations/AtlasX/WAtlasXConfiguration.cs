using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class WAtlasXConfiguration : EntityTypeConfiguration<WAtlasX>
    {
        public WAtlasXConfiguration()
        {
            ToTable("tbWAtlasX");
            HasKey(t => t.AtlasXId);
            Property(t => t.AtlasXId).HasColumnName("WAtlasXID").IsRequired();
            Property(t => t.PropertyId).HasColumnName("PropertyID").IsRequired();
            Property(t => t.CreatorPersonNumber).HasColumnName("CreatorWPersno").IsRequired();
            Property(t => t.AtlasXDate).HasColumnName("WAtlasXDate").IsRequired();
            Property(t => t.Active).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(p => p.CrUser).HasColumnName("CRuser").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(p => p.CrDate).HasColumnName("CRdate").IsRequired();
            Property(p => p.ChUser).HasColumnName("CHuser").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.ChDate).HasColumnName("CHdate").IsOptional();

            Ignore(p => p.EntityStateForGraphsUpdates);
        }
    }
}
