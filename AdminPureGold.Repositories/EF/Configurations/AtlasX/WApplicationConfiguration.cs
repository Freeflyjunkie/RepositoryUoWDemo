using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class WApplicationConfiguration : EntityTypeConfiguration<WApplication>
    {
        public WApplicationConfiguration()
        {
            ToTable("tbApplication");
            HasKey(t => t.AppNameId);
            Property(t => t.AppNameId).HasColumnName("AppNameID").IsRequired();
            Property(t => t.AppName).HasColumnType("varchar").HasMaxLength(50).IsRequired();            
        }
    }
}
