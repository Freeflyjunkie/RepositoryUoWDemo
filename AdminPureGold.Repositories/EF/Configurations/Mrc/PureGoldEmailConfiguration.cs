using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PureGoldEmailConfiguration : EntityTypeConfiguration<PureGoldEmail>
    {
        public PureGoldEmailConfiguration()
        {
            ToTable("tbPG_Email");
            HasKey(t => t.TransactionId);
            Property(t => t.TransactionId).HasColumnName("TransactionID").HasColumnType("int").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPersNo").HasColumnType("int").IsRequired();
            Property(t => t.RelationshipNumber).HasColumnName("WRelateNo").HasColumnType("int").IsRequired();
            Property(t => t.ReferenceNumber).HasColumnName("ReferenceNumber").HasColumnType("varchar").HasMaxLength(15).IsOptional();
            Property(t => t.EnvelopeName).HasColumnName("EnvelopeName").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.Address1).HasColumnName("Address1").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.City).HasColumnName("City").HasColumnType("varchar").HasMaxLength(100).IsOptional();
            Property(t => t.AddressState).HasColumnName("State").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(t => t.Zipcode).HasColumnName("Zipcode").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.AppObjectId).HasColumnName("AppObjectID").HasColumnType("int").IsRequired();
            Property(t => t.PrintType).HasColumnName("PrintType").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.PrintDate).HasColumnName("PrintDate").HasColumnType("smalldatetime").IsOptional();
            Property(t => t.AssociateName).HasColumnName("AssociateName").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.EmailSentDate).HasColumnName("EmailSentDate").HasColumnType("smalldatetime").IsOptional();
            Property(t => t.InputDate).HasColumnName("InputDate").HasColumnType("smalldatetime").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
