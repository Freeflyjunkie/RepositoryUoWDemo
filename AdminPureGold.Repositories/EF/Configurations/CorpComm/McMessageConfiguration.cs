using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.CorpComm;

namespace AdminPureGold.Repositories.EF.Configurations.CorpComm
{
    class McMessageConfiguration : EntityTypeConfiguration<McMessage>
    {
        public McMessageConfiguration()
        {
            ToTable("tbMCMessage");
            HasKey(t => t.MessageId);
            Property(t => t.MessageId).HasColumnName("MessageID").IsRequired();
            Property(t => t.RecipientWpersno).HasColumnName("RecipientWpersno").IsRequired();
            Property(t => t.SenderWpersno).HasColumnName("SenderWpersno").IsOptional();
            Property(t => t.CreateDate).HasColumnName("CreateDate").HasColumnType("smalldatetime").IsRequired();
            Property(t => t.AcknowledgeDate).HasColumnName("AcknowledgeDate").HasColumnType("smalldatetime").IsOptional();
            Property(t => t.DueDate).HasColumnName("DueDate").HasColumnType("smalldatetime").IsOptional();
            Property(t => t.DoNotDisplay).HasColumnName("DoNotDisplay").IsOptional();
            Property(t => t.AppId).HasColumnName("AppID").IsOptional();
            Property(t => t.AppSubId).HasColumnName("AppSubID").IsOptional();
            Property(t => t.SubjectText).HasColumnName("SubjectText").HasColumnType("varchar").HasMaxLength(500).IsOptional();
            Property(t => t.MessageLink).HasColumnName("MessageLink").HasColumnType("varchar").HasMaxLength(500).IsOptional();
            Property(t => t.MessageLinkTarget).HasColumnName("MessageLinkTarget").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(t => t.OriginalMessageId).HasColumnName("OriginalMessageID").IsOptional();
            Property(t => t.MessageBody).HasColumnName("MessageBody").HasColumnType("varchar").IsOptional();
            Property(t => t.Priority).HasColumnName("Priority").IsOptional();
            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
