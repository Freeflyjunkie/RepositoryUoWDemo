using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class ListPropertyConfiguration : EntityTypeConfiguration<ListProperty>
    {
        public ListPropertyConfiguration()
        {
            ToTable("tbListProperty");
            HasKey(p => p.ListId);
            Property(p => p.ListId).HasColumnName("ListID").IsRequired();
            Property(p => p.AtlasXPropertyId).HasColumnName("AtlasXPropertyID").IsOptional();
            Property(p => p.Address1).HasColumnName("Address1").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.Address2).HasColumnName("Address2").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.City).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.State).HasColumnType("varchar").HasMaxLength(2).IsOptional();
            Property(p => p.ZipCode).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.Block).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.Lot).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.ChDate).HasColumnName("CHDATE").IsOptional();
        }
    }
}
