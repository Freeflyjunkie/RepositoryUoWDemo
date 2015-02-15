using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class TransactionToRelateConfiguration : EntityTypeConfiguration<TransactionToRelate>
    {
        public TransactionToRelateConfiguration()
        {
            ToTable("tbTransactionToRelate");
            HasKey(t => t.TransactionToRelateId);
            Property(t => t.TransactionToRelateId).HasColumnName("TransactionToRelateID").IsRequired();

            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.RelationshipNumber).HasColumnName("WRelateNo").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPersNo").IsRequired();
            Property(t => t.OfficeId).HasColumnName("OfficeID").IsOptional();
            Property(t => t.Active).IsRequired();
            Property(t => t.PayAmount).IsRequired();
            Property(t => t.SortOrder).IsRequired();
            Property(t => t.CrtBy).HasColumnName("CRT_BY").IsRequired();
            Property(t => t.CrtDt).HasColumnName("CRT_DT").IsRequired();
            Property(t => t.UpdBy).HasColumnName("UPD_BY").IsOptional();
            Property(t => t.UpdDt).HasColumnName("UPD_DT").IsOptional();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }    
}
