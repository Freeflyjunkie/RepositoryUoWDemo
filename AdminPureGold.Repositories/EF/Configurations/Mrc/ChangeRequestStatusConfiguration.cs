using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class ChangeRequestStatusConfiguration : EntityTypeConfiguration<ChangeRequestStatus>
    {
        public ChangeRequestStatusConfiguration()
        {
            ToTable("tbChangeStatus");
            HasKey(t => t.ChangeRequestStatusId);
            Property(t => t.ChangeRequestStatusId).HasColumnName("ChangeStatusID").IsRequired();
            Property(t => t.ChangeRequestDescription).HasColumnName("ChangeStatusDesc").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
