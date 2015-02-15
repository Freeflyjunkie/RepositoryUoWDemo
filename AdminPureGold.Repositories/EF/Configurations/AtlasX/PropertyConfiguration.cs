using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.AtlasX;

namespace AdminPureGold.Repositories.EF.Configurations.AtlasX
{
    class PropertyConfiguration : EntityTypeConfiguration<Property>
    {
        public PropertyConfiguration()
        {
            ToTable("tbProperty");
            HasKey(p => p.PropertyId);
            Property(p => p.PropertyId).HasColumnName("PropertyID").IsRequired();
            Property(p => p.PropertyTypeId).HasColumnName("PropertyTypeID").IsOptional();
            Property(p => p.Address1).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(p => p.Address2).HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(p => p.City).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            Property(p => p.State).HasColumnType("char").HasMaxLength(2).IsRequired();
            Property(p => p.Zip).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            Property(p => p.Zip4).HasColumnType("varchar").HasMaxLength(4).IsOptional();
            Property(p => p.CountyName).HasColumnType("varchar").HasMaxLength(30).IsOptional();
            Property(p => p.Longitude).IsOptional();
            Property(p => p.Latitude).IsOptional();
            Property(p => p.Block).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.Lot).HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(p => p.FragmentHouse).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.FragmentStreet).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.FragmentSuffix).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.FragmentPreDir).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.FragmentPostDir).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.FragmentUnit).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.Fragment).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.BarcodeDigits).HasColumnType("varchar").HasMaxLength(30).IsOptional();
            Property(p => p.Validated).IsRequired();
            Property(p => p.ValidationExempt).IsRequired();
            Property(p => p.MatchedValidPropertyId).HasColumnName("MatchedValidPropertyID").IsOptional();
            Property(p => p.CRapplicationId).IsOptional();
            Property(p => p.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(p => p.CrDate).HasColumnName("CRDATE").IsOptional();
            Property(p => p.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.ChDate).HasColumnName("CHDATE").IsOptional();

            Ignore(p => p.EntityStateForGraphsUpdates);
        }
    }
}
