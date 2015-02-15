using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class TransactionToContactConfiguration : EntityTypeConfiguration<TransactionToContact>
    {
        public TransactionToContactConfiguration()
        {
            ToTable("tbTransactionToContact");
            HasKey(t => t.TransactionToContactId);
            Property(t => t.TransactionToContactId).HasColumnName("TransactionToContactID").IsRequired();
            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.AtlasXContactId).HasColumnName("AtlasContactID");
            Property(t => t.CrtBy).HasColumnName("CRT_BY").IsRequired();
            Property(t => t.CrtDt).HasColumnName("CRT_DT").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }  
}
