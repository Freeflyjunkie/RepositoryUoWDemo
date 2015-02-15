using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintJobToAppObjectToTransactionConfiguration : EntityTypeConfiguration<PrintJobToAppObjectToTransaction>
    {
        public PrintJobToAppObjectToTransactionConfiguration()
        {
            ToTable("tbPG_PrintJobToAppObjectToTransaction");
            HasKey(t => t.PrintJobToAppObjectToTransactionId);
            Property(t => t.PrintJobToAppObjectToTransactionId).HasColumnName("PrintJobToAppObjectToTransactionID").IsRequired();
            Property(t => t.PrintJobId).HasColumnName("PrintJobID").IsRequired();
            Property(t => t.AppObjectToTransactionId).HasColumnName("AppObjectToTransactionID").IsRequired();
            Property(t => t.PrintJobItemStatusId).HasColumnName("PrintJobItemStatusID").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
