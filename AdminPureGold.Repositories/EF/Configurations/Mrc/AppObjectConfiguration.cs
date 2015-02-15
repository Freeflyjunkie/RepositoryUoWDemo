using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class AppObjectConfiguration : EntityTypeConfiguration<AppObject>
    {
        public AppObjectConfiguration()
        {
            ToTable("tbAppObject");
            HasKey(t => t.AppObjectId);
            Property(t => t.AppObjectId).HasColumnName("AppObjectID").IsRequired();
            Property(t => t.AppObjectName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            Property(t => t.AppObjectDesc).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            Property(t => t.Active).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(t => t.AddedBy).IsRequired();
            Property(t => t.AddedDate).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
