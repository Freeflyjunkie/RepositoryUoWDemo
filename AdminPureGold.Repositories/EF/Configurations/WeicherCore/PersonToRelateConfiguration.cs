using AdminPureGold.Domain.Models.WeichertCore;
using System.Data.Entity.ModelConfiguration;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class PersonToRelateConfiguration : EntityTypeConfiguration<PersonToRelate>
    {
        public PersonToRelateConfiguration()
        {
            ToTable("tbWPersonToRelate");
            HasKey(t => t.RelationshipNumber);
            Property(t => t.RelationshipNumber).HasColumnName("WRELATENO").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPERSNO").IsRequired();
            Property(t => t.RoleTaskNumber).HasColumnName("ROLETASKNO").IsRequired();
            Property(t => t.OfficeId).HasColumnName("OfficeID").IsRequired();
            Property(t => t.Active).HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(t => t.StartDate).HasColumnName("COSTARTDATE").IsOptional();
            Property(t => t.Anniversary).HasColumnName("ANNIVERSARY").IsOptional();
            Property(t => t.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.CrDate).HasColumnName("CRDATE").HasColumnType("datetime").IsOptional();
            Property(t => t.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.ChDate).HasColumnName("CHDATE").HasColumnType("datetime").IsOptional();
        }
    }
}
