using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class TransactionTemplateConfiguration : EntityTypeConfiguration<TransactionTemplate>
    {
        public TransactionTemplateConfiguration()
        {
            ToTable("tbTransactionTemplate");
            HasKey(t => t.TransactionTemplateId);
            Property(t => t.TransactionTemplateId).HasColumnName("TransactionTemplateID").IsRequired();
            Property(t => t.AppObjectToTransactionId).HasColumnName("AppObjectToTransactionID").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
