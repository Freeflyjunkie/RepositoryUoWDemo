using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class WAtlasXtoAppWPersonConfiguration : EntityTypeConfiguration<WAtlasXtoAppWPerson>
    {
        public WAtlasXtoAppWPersonConfiguration()
        {
            ToTable("tbWAtlasXtoAppWPerson");
            HasKey(t => t.AtlasXtoAppWPersonId);
            Property(t => t.AtlasXtoAppWPersonId).HasColumnName("WAtlasXtoAppWPersonID").IsRequired();
            Property(t => t.AtlasXtoAppId).HasColumnName("WAtlasXtoAppID").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPersno").IsRequired();
            Property(t => t.RelationshipNumber).HasColumnName("WRelateNo").IsRequired();
            Property(t => t.AtlasXtoAppRoleId).HasColumnName("WAtlasXtoAppRoleID").IsRequired();
            Property(t => t.Active).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(p => p.CrUser).HasColumnName("CRuser").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(p => p.CrDate).HasColumnName("CRdate").IsRequired();
            Property(p => p.ChUser).HasColumnName("CHuser").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.ChDate).HasColumnName("CHdate").IsOptional();

            Ignore(p => p.EntityStateForGraphsUpdates);
        }
    }
}
