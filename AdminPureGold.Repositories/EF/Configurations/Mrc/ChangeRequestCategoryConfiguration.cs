using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class ChangeRequestCategoryConfiguration : EntityTypeConfiguration<ChangeRequestCategory>
    {
        public ChangeRequestCategoryConfiguration()
        {
            ToTable("tbChangeCategory");
            HasKey(t => t.ChangeRequestCategoryId);
            Property(t => t.ChangeRequestCategoryId).HasColumnName("ChangeCategoryID").IsRequired();
            Property(t => t.ChangeRequestDescription).HasColumnName("ChangeCategoryDesc").HasColumnType("varchar").HasMaxLength(25).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
