using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class ChangeRequestCommentConfiguration : EntityTypeConfiguration<ChangeRequestComment>
    {
        public ChangeRequestCommentConfiguration()
        {
            ToTable("tbChangeComment");
            HasKey(t => t.ChangeRequestCommentId);
            Property(t => t.ChangeRequestCommentId).HasColumnName("ChangeCommentID").IsRequired();
            Property(t => t.ChangeRequestId).HasColumnName("ChangeRequestID").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("Wpersno").IsRequired();
            Property(t => t.Comment).HasColumnType("varchar").HasMaxLength(2000).IsRequired();
            Property(t => t.CrDate).HasColumnName("CRdate").IsOptional();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
