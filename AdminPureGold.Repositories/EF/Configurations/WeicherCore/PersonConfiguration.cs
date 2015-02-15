using AdminPureGold.Domain.Models.WeichertCore;
using System.Data.Entity.ModelConfiguration;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            ToTable("tbWPerson");
            HasKey(t => t.PersonNumber);
            Property(t => t.PersonNumber).HasColumnName("WPERSNO").IsRequired();            
            Property(t => t.Birthday).HasColumnName("BIRTHDAY").IsOptional();
            Property(t => t.Active).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(t => t.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.CrDate).HasColumnName("CRDATE").HasColumnType("datetime").IsOptional();
            Property(t => t.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.ChDate).HasColumnName("CHDATE").HasColumnType("datetime").IsOptional();
        }
    }
}
