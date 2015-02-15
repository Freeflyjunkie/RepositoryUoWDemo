using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintJobItemStatusConfiguration : EntityTypeConfiguration<PrintJobItemStatus>
    {
        public PrintJobItemStatusConfiguration()
        {
            ToTable("tbPG_PrintJobItemStatus");
            HasKey(t => t.PrintJobItemStatusId);
            Property(t => t.PrintJobItemStatusId).HasColumnName("PrintJobItemStatusID").IsRequired();
            Property(t => t.PrintJobItemStatusDesc).HasColumnName("PrintJobItemStatusDesc").HasColumnType("varchar").HasMaxLength(200).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
