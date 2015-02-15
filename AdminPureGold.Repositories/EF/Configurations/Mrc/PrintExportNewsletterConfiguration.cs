using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintExportNewsletterConfiguration : EntityTypeConfiguration<PrintExportNewsletter>
    {
        public PrintExportNewsletterConfiguration()
        {
            ToTable("tbPrintExport_Newsletter");
            HasKey(t => t.PureGoldId);
            Property(t => t.PureGoldId).HasColumnName("PureGoldID").HasColumnType("int").IsRequired();            
            Property(t => t.Associate1LastName).HasColumnName("Associate1LastName").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.Associate1Name).HasColumnName("Associate1Name").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.Associate2Name).HasColumnName("Associate2Name").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.OfficeName).HasColumnName("OfficeName").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.OfficeAddress).HasColumnName("OfficeAddress").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.OfficeCity).HasColumnName("OfficeCity").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.OfficeState).HasColumnName("OfficeState").HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(t => t.OfficeZip).HasColumnName("OfficeZip").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(t => t.OfficePhone).HasColumnName("OfficePhone").HasColumnType("varchar").HasMaxLength(14).IsOptional();
            Property(t => t.AssociateCell).HasColumnName("AssociateCell").HasColumnType("varchar").HasMaxLength(20).IsOptional();
            Property(t => t.AssociateEmail).HasColumnName("AssociateEmail").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.Envelope).HasColumnName("Envelope").HasColumnType("varchar").HasMaxLength(300).IsOptional();
            Property(t => t.Address1).HasColumnName("Address1").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.Address2).HasColumnName("Address2").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(200).IsOptional();
            Property(t => t.State).HasColumnName("").HasColumnType("char").HasMaxLength(3).IsOptional();
            Property(t => t.ZipCode).HasColumnName("ZipCode").HasColumnType("varchar").HasMaxLength(15).IsOptional();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
