using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class WAtlasXtoAppRoleConfiguration : EntityTypeConfiguration<WAtlasXtoAppRole>
    {
        public WAtlasXtoAppRoleConfiguration()
        {
            ToTable("tbWAtlasXtoAppRole");
            HasKey(t => t.AtlasXtoAppRoleId);
            Property(t => t.AtlasXtoAppRoleId).HasColumnName("WAtlasXtoAppRoleID").IsRequired();
            Property(t => t.AtlasXtoAppRoleDesc).HasColumnName("WAtlasXtoAppRoleDesc").HasColumnType("varchar").HasMaxLength(40).IsRequired();            
        }
    }
}
