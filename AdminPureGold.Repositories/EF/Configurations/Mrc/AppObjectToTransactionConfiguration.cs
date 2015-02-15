using AdminPureGold.Domain.Models.Mrc;
using System.Data.Entity.ModelConfiguration;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class AppObjectToTransactionConfiguration : EntityTypeConfiguration<AppObjectToTransaction>
    {
        public AppObjectToTransactionConfiguration()
        {
            ToTable("tbAppObjectToTransaction");            
            HasKey(t => t.AppObjectToTransactionId);            
            Property(t => t.AppObjectToTransactionId).HasColumnName("AppObjectToTransactionID").IsRequired();
            Property(t => t.AppObjectId).HasColumnName("AppObjectID").IsRequired();
            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.CrtBy).HasColumnName("CRT_BY").IsRequired();
            Property(t => t.CrtDt).HasColumnName("CRT_DT").IsRequired();
            Property(t => t.UpdBy).HasColumnName("UPD_BY").IsRequired();
            Property(t => t.UpdDt).HasColumnName("UPD_DT").IsRequired();                       

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
