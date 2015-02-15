using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintExportConfiguration : EntityTypeConfiguration<PrintExport>
    {
        public PrintExportConfiguration()
        {
            ToTable("tbPrintExport");
            HasKey(t => t.PureGoldId);
            Property(t => t.PureGoldId).HasColumnName("PureGoldID").HasColumnType("bigint").IsRequired();
            Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(t => t.Rvp).HasColumnName("RVP").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.SalesAssociatesNumbers).HasColumnName("SalesAssociatesNumbers").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.SalesAssociates).HasColumnName("SalesAssociates").HasColumnType("varchar").HasMaxLength(300).IsOptional();
            Property(t => t.SalesAssociateFirstNames).HasColumnName("SalesAssociateFirstNames").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.SalesAssociateLastName).HasColumnName("SalesAssociateLastName").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.Verb).HasColumnName("Verb").HasColumnType("varchar").HasMaxLength(5).IsOptional();
            Property(t => t.OfficeLongCode).HasColumnName("OfficeLongCode").HasColumnType("varchar").HasMaxLength(6).IsOptional();
            Property(t => t.OfficeName).HasColumnName("OfficeName").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.OfficeAddress).HasColumnName("OfficeAddress").HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(t => t.OfficeCity).HasColumnName("OfficeCity").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.OfficeState).HasColumnName("OfficeState").HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(t => t.OfficeZip).HasColumnName("OfficeZip").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(t => t.Salutation).HasColumnName("Salutation").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.Envelope).HasColumnName("Envelope").HasColumnType("varchar").HasMaxLength(300).IsOptional();
            Property(t => t.Address1).HasColumnName("Address1").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.Address2).HasColumnName("Address2").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.State).HasColumnName("State").HasColumnType("char").HasMaxLength(3).IsOptional();
            Property(t => t.ZipCode).HasColumnName("ZipCode").HasColumnType("varchar").HasMaxLength(15).IsOptional();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
