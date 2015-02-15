using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class AppObjectToAttributeConfiguration : EntityTypeConfiguration<AppObjectToAttribute>
    {
        public AppObjectToAttributeConfiguration()
        {
            ToTable("tbAppObjectToAttribute");
            HasKey(t => t.AppObjectToAttributeId);
            Property(t => t.AppObjectToAttributeId).HasColumnName("AppObjectToAttributeID").IsRequired();
            Property(t => t.AttributeTypeId).HasColumnName("AttributeTypeID").IsRequired();
            Property(t => t.AppObjectId).HasColumnName("AppObjectID").IsRequired();
            Property(t => t.AttributeValue).HasColumnType("varchar").HasMaxLength(2000).IsRequired();
            Property(t => t.AddedBy).IsRequired();
            Property(t => t.AddedDate).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
