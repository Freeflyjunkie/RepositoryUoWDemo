using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class RelateToEmailConfiguration: EntityTypeConfiguration<RelateToEmail>
    {
        public RelateToEmailConfiguration()
        {
            ToTable("tbRelateToEmail");
            HasKey(t => t.RelateToEmailId);
            Property(t => t.RelateToEmailId)
                .HasColumnName("RelateToEmailID")
                .IsRequired();
            Property(t => t.RelationshipNumber)
                .HasColumnName("WRELATENO")
                .IsOptional();
            Property(t => t.EmailRoleCode)
                .HasColumnName("EMAILROLECD")
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsOptional();
            Property(t => t.EmailAddress)
                .HasColumnName("EMAILADDR")
                .HasColumnType("varchar")
                .HasMaxLength(75)
                .IsOptional();
            Property(t => t.CrUser)
                .HasColumnName("CRUSER")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsOptional();
            Property(t => t.CrDate)
                .HasColumnName("CRDATE")
                .HasColumnType("datetime")
                .IsOptional();
            Property(t => t.ChUser)
                .HasColumnName("CHUSER")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsOptional();
            Property(t => t.ChDate)
                .HasColumnName("CHDATE")
                .HasColumnType("datetime")
                .IsOptional();
        }
    }
}
