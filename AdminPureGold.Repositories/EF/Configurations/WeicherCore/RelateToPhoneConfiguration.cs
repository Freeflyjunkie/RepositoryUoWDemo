using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class RelateToPhoneConfiguration: EntityTypeConfiguration<RelateToPhone>
    {
        public RelateToPhoneConfiguration()
        {
            ToTable("tbRelateToPhone");
            HasKey(t => t.RelateToPhoneId);
            Property(t => t.RelateToPhoneId)
                .HasColumnName("RelateToPhoneID")
                .IsRequired();
            Property(t => t.RelationshipNumber)
                .HasColumnName("WRELATENO")
                .IsOptional();
            Property(t => t.PhoneRoleCode)
                .HasColumnName("PHONEROLECD")
                .HasColumnType("varchar")
                .HasMaxLength(8)
                .IsOptional();
            Property(t => t.PhoneNumber)
                .HasColumnName("PHONENUM")
                .HasColumnType("char")
                .HasMaxLength(10)
                .IsOptional();
            Property(t => t.PhoneNumberExtension)
                .HasColumnName("PHONENUMEXT")
                .HasColumnType("char")
                .HasMaxLength(5)
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
