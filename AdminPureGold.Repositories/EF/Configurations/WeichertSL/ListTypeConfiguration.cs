using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class ListTypeConfiguration : EntityTypeConfiguration<ListType>
    {
        public ListTypeConfiguration()
        {
            ToTable("tbListType");
            HasKey(p => p.ListTypeId);
            Property(p => p.ListTypeId).HasColumnName("ListTypeID").IsRequired();
            Property(p => p.ListTypeDescription)
                .HasColumnName("ListTypeDesc")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();
        }
    }
}
