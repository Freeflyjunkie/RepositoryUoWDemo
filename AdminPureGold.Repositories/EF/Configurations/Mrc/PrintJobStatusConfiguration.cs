using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintJobStatusConfiguration : EntityTypeConfiguration<PrintJobStatus>
    {
        public PrintJobStatusConfiguration()
        {
            ToTable("tbPG_PrintJobStatus");
            HasKey(t => t.PrintJobStatusId);
            Property(t => t.PrintJobStatusId).HasColumnName("PrintJobStatusID").IsRequired();
            Property(t => t.PrintJobStatusDesc).HasColumnName("PrintJobStatusDesc").HasColumnType("varchar").HasMaxLength(200).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
