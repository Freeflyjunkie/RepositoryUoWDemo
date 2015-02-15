using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class WeichertOneUserConfiguration : EntityTypeConfiguration<WeichertOneUser>
    {
        public WeichertOneUserConfiguration()
        {
            ToTable("tbW1User");
            HasKey(w => w.PersonNumber);
            Property(w => w.PersonNumber).HasColumnName("WPERSNO").HasColumnType("int").IsRequired();
            Property(w => w.Login).HasColumnName("LOGIN").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(w => w.Active).HasColumnName("ACTIVE").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(w => w.FirstAccess).HasColumnName("FIRSTACCESS").HasColumnType("datetime");
            Property(w => w.LastAccess).HasColumnName("LASTACCESS").HasColumnType("datetime");
            Property(w => w.LockedOut).HasColumnName("LockedOut").HasColumnType("bit");
            Property(w => w.LockedOutDate).HasColumnName("LockedOutDate").HasColumnType("datetime");
            Property(w => w.FailedLoginCount).HasColumnName("FailedLoginCount").HasColumnType("int");
            Property(w => w.PasswordChangeDate).HasColumnName("PasswordChangeDate").HasColumnType("datetime");
            Property(w => w.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25);
            Property(w => w.CrDate).HasColumnName("CRDATE").HasColumnType("datetime");
            Property(w => w.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25);
            Property(w => w.ChDate).HasColumnName("CHDATE").HasColumnType("datetime");

        }
    }
}
