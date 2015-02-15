using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class ChangeRequestConfiguration: EntityTypeConfiguration<ChangeRequest>
    {
        public ChangeRequestConfiguration()
        {
            ToTable("tbChangeRequest");
            HasKey(t => t.ChangeRequestId);
            Property(t => t.ChangeRequestId).HasColumnName("ChangeRequestID").IsRequired();
            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.ChangeRequestCategoryId).HasColumnName("ChangeCategoryID").IsRequired();
            Property(t => t.ChangeRequestStatusId).HasColumnName("ChangeStatusID").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("Wpersno").IsRequired();
            Property(t => t.Detail).HasColumnName("ChangeDetail").HasColumnType("varchar").HasMaxLength(2000).IsRequired();
            Property(t => t.CrUser).HasColumnName("CRuser").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(t => t.CrDate).HasColumnName("CRdate").IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
