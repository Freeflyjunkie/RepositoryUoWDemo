using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF.Configurations.WeicherCore
{
    class PersonToImageConfiguration : EntityTypeConfiguration<PersonToImage>
    {
        public PersonToImageConfiguration()
        {
            ToTable("tbWPersonToImage");
            HasKey(t => t.PersonToImageId);
            Property(t => t.PersonToImageId).HasColumnName("WPersonToImageID").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPERSNO").IsRequired();
            Property(t => t.ImagePath).HasColumnName("ImagePath").HasColumnType("varchar").HasMaxLength(500).IsRequired();
            Property(t => t.ImageDescription).HasColumnName("ImageDesc").HasColumnType("varchar").HasMaxLength(250).IsOptional();
            Property(t => t.DisplayStatus)
                .HasColumnName("DisplayStatus")
                .HasColumnType("varchar")
                .HasMaxLength(1)
                .IsRequired();
            Property(t => t.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(t => t.CrDate).HasColumnName("CRDATE").HasColumnType("datetime").IsRequired();
            Property(t => t.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(t => t.ChDate).HasColumnName("CHDATE").HasColumnType("datetime").IsOptional();
        }
    }
}
