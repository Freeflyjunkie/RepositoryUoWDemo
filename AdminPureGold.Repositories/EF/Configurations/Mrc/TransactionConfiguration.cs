using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class TransactionConfiguration : EntityTypeConfiguration<Transaction>
    {
        public TransactionConfiguration()
        {
            ToTable("tbTransaction");
            HasKey(t => t.TransactionId);                        
            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPersNo").IsRequired();
            Property(t => t.AtlasXPropertyId).HasColumnName("AtlasXPropertyID").IsRequired();
            Property(t => t.CrtDt).HasColumnName("CRT_DT").IsRequired();
            Property(t => t.Active).HasColumnName("Active").IsRequired();
            Property(t => t.AtlasXPropertyAlternateId).HasColumnName("AtlasXPropertyAlternateID");

            HasOptional(t => t.PresentationDetail).WithRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);            
        }
    }
}
