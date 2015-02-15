using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PureGoldEmailSettingsConfiguration : EntityTypeConfiguration<PureGoldEmailSetting>
    {
        public PureGoldEmailSettingsConfiguration()
        {
            ToTable("tbPG_EmailSettings");
            HasKey(t => t.EmailSettingId);
            Property(t => t.SendEmailsFlag).HasColumnName("EmailSettingId").IsRequired();
            Property(t => t.SendEmailsFlag).HasColumnName("SendEmailsFlag").HasColumnType("char").HasMaxLength(1).IsRequired();
            Property(t => t.EmailSubject).HasColumnName("EmailSubject").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.EmailBodyTop).HasColumnName("EmailBodyTop").HasColumnType("varchar").HasMaxLength(6000).IsOptional();
            Property(t => t.EmailBodyBottom).HasColumnName("EmailBodyBottom").HasColumnType("varchar").HasMaxLength(6000).IsOptional();
            Property(t => t.MessageCenterMessage).HasColumnName("MessageCenterMessage").HasColumnType("varchar").HasMaxLength(6000).IsOptional();
            Property(t => t.CurrentDueDate).HasColumnName("CurrentDueDate").HasColumnType("date").IsOptional();
            Property(t => t.SenderName).HasColumnName("SenderName").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.SenderEmail).HasColumnName("SenderEmail").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.EmailServer).HasColumnName("EmailServer").HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(t => t.ErrorRecipients).HasColumnName("ErrorRecipients").HasColumnType("varchar").HasMaxLength(1000).IsOptional();
            Ignore(t => t.EntityStateForGraphsUpdates);
        }

    }
}
