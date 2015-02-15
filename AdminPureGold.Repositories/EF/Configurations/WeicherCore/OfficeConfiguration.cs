using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class OfficeConfiguration : EntityTypeConfiguration<Office>
    {
        public OfficeConfiguration()
        {
            ToTable("tbOfficeMaster");
            HasKey(o => o.OfficeId);
            Property(o => o.OfficeId).HasColumnName("OfficeID");
            Property(o => o.OfficeLongNumber)
                .HasColumnName("OfficeLongNo")
                .HasColumnType("varchar")
                .HasMaxLength(6)
                .IsRequired();
            Property(o => o.Division).HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(o => o.DivisionName).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(o => o.Region).HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(o => o.Rvp).HasColumnName("RVP").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(o => o.OfficeNumber).HasColumnType("char").HasMaxLength(3).IsOptional();
            Property(o => o.OfficeName).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(o => o.AbbreviatedName)
                .HasColumnName("AbrvName")
                .HasColumnType("char")
                .HasMaxLength(10)
                .IsOptional();
            Property(o => o.State).HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(o => o.Address).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(o => o.City).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(o => o.ZipCode).HasColumnType("char").HasMaxLength(10).IsOptional();
            Property(o => o.Fax).HasColumnType("char").HasMaxLength(10).IsOptional();
            Property(o => o.OfficeType).HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(o => o.OfficeStatus).HasColumnType("char").HasMaxLength(1).IsOptional();
            Property(o => o.County).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(o => o.Directions).HasColumnType("varchar").HasMaxLength(3000).IsOptional();
            Property(o => o.FriendlyOfficeName).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(o => o.FriendlyDivisionName)
                .HasColumnName("FriendlyDivName")
                .HasColumnType("varchar")
                .HasMaxLength(30)
                .IsOptional();
        }       
    }
}
