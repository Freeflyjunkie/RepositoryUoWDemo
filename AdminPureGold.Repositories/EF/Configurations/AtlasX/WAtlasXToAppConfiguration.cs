using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class WAtlasXToAppConfiguration : EntityTypeConfiguration<WAtlasXToApp>
    {
        public WAtlasXToAppConfiguration()
        {
            ToTable("tbWAtlasXtoApp");
            HasKey(t => t.AtlasXtoAppId);
            Property(t => t.AtlasXtoAppId).HasColumnName("WAtlasXtoAppID").IsRequired();
            Property(t => t.AtlasXId).HasColumnName("WAtlasXID").IsRequired();
            Property(t => t.AppNameId).HasColumnName("AppNameID").IsRequired();
            Property(t => t.AppXId).HasColumnName("AppXID").IsRequired();
            Property(t => t.Active).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(p => p.CrUser).HasColumnName("CRuser").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(p => p.CrDate).HasColumnName("CRdate").IsRequired();
            Property(p => p.ChUser).HasColumnName("CHuser").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.ChDate).HasColumnName("CHdate").IsOptional();

            Ignore(p => p.EntityStateForGraphsUpdates);
        }
    }
}
