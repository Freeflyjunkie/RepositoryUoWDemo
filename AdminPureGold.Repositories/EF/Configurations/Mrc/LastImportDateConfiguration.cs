using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class LastImportDateConfiguration : EntityTypeConfiguration<LastImportDate>
    {
        public LastImportDateConfiguration()
        {
            ToTable("tbLastImportDate");            
            Property(t => t.LastImportDateId).HasColumnName("autoID");
            Property(t => t.ImportDate).HasColumnName("LastImportDate");

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
