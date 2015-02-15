using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PureGoldMailingConfiguration : EntityTypeConfiguration<PureGoldMailing>
    {
        public PureGoldMailingConfiguration()
        {
            ToTable("tbPG_Mailing");

            HasKey(t => t.MailingId);
            Property(t => t.MailingId).HasColumnName("MailingID").HasColumnType("int").IsRequired();            
            Property(t => t.AppObjectToTransactionId).HasColumnName("AppObjectToTransactionID").HasColumnType("int").IsRequired();
            Property(t => t.ScheduledPrintDate).HasColumnName("ScheduledPrintDate").HasColumnType("date").IsRequired();
            Property(t => t.ActualPrintDate).HasColumnName("ActualPrintDate").HasColumnType("date").IsOptional();            

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
