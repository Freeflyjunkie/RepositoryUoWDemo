using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class UserRequestLogConfiguration : EntityTypeConfiguration<UserRequestLog>
    {
        public UserRequestLogConfiguration()
        {
            ToTable("tbPG_UserRequestLog");
            HasKey(t => t.UserRequestLogId);
            Property(t => t.UserRequestLogId).HasColumnName("UserRequestLogID").IsRequired();
            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.IsChangeRequest).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
