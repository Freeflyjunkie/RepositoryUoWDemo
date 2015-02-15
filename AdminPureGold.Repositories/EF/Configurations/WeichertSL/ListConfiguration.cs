using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class ListConfiguration : EntityTypeConfiguration<List>
    {
        public ListConfiguration()
        {
            ToTable("tbList");
            HasKey(l => l.ListId);
            Property(l => l.ListId).IsRequired();

            Property(l => l.ReferenceNumberNumeric).HasColumnName("intRefNum").IsOptional();
            Property(l => l.ListTypeId).HasColumnName("ListTypeID").IsOptional();
            Property(l => l.OfficeId).HasColumnName("OfficeID").IsOptional();
            Property(l => l.WxFlag).HasColumnName("WXflag").IsOptional();
            Property(l => l.ListSourceCode)
                .HasColumnName("ListSourceCD")
                .HasColumnType("varchar")
                .HasMaxLength(5)
                .IsOptional();
            Property(l => l.ListPrice).IsOptional();
            Property(l => l.ListStatus).HasColumnType("char").HasMaxLength(1).IsOptional();
            Property(l => l.ListEntryDate).IsOptional();
            Property(l => l.ListContractDate).IsOptional();
            Property(l => l.ListExpDate).IsOptional();
            Property(l => l.Municipality).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(l => l.ReferenceNumber).HasColumnType("char").HasMaxLength(9).IsRequired();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();

            HasOptional(l => l.ListProperty).WithRequired();

            HasMany(l => l.ListToAssociates).WithRequired().HasForeignKey(l => l.ListId);
            HasMany(l => l.ListToSellers).WithRequired().HasForeignKey(l => l.ListId);
            HasMany(l => l.Sales).WithRequired().HasForeignKey(l => l.ListId);

            HasOptional(cd => cd.SourceCd).WithMany().HasForeignKey(s => s.ListSourceCode);
        }
    }
}
