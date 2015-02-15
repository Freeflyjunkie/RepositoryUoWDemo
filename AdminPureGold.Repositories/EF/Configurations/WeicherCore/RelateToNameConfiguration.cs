using AdminPureGold.Domain.Models.WeichertCore;
using System.Data.Entity.ModelConfiguration;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class RelateToNameConfiguration : EntityTypeConfiguration<RelateToName>
    {
        public RelateToNameConfiguration()
        {
            ToTable("tbRelateToName");
            HasKey(t => t.RelateToNameId);
            Property(t => t.RelateToNameId)
                .HasColumnName("RelateToNameID")
                .IsRequired();
            Property(t => t.RelationshipNumber)
                .HasColumnName("WRELATENO")
                .IsRequired();
            Property(t => t.NameRoleCode)
                .HasColumnName("NAMEROLECD")
                .HasColumnType("varchar")
                .HasMaxLength(8)
                .IsRequired();
            Property(t => t.FirstName)
                .HasColumnName("FNAME")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();
            Property(t => t.MiddleName)
                .HasColumnName("MNAME")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsOptional();
            Property(t => t.LastName)
                .HasColumnName("LNAME")
                .HasColumnType("varchar")
                .HasMaxLength(35)
                .IsRequired();
            Property(t => t.Suffix)
                .HasColumnName("SUFFIX")
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsOptional();
            Property(t => t.CrUser)
                .HasColumnName("CRUSER")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();
            Property(t => t.CrDate)
                .HasColumnName("CRDATE")
                .HasColumnType("datetime")
                .IsRequired();
            Property(t => t.ChUser)
                .HasColumnName("CHUSER")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();
            Property(t => t.ChDate)
                .HasColumnName("CHDATE")
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
