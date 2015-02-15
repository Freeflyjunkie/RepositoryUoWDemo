using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintJobToWeichertSLConfiguration : EntityTypeConfiguration<PrintJobToWeichertSL>
    {
        public PrintJobToWeichertSLConfiguration()
        {
            ToTable("tbPG_PrintJobToWeichertSL");
            HasKey(t => t.PrintJobToWeichertSlId);
            Property(t => t.PrintJobToWeichertSlId).HasColumnName("PrintJobToWeichertSLID").IsRequired();
            Property(t => t.PrintJobId).HasColumnName("PrintJobID").IsRequired();
            Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber").HasColumnType("varchar").HasMaxLength(9).IsRequired();
            Property(t => t.PrintJobItemStatusId).HasColumnName("PrintJobItemStatusID").IsRequired();
            Property(t => t.SaleId).HasColumnName("WeichertSLSaleID").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
