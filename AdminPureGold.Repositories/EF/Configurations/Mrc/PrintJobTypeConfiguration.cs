using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;


namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintJobTypeConfiguration : EntityTypeConfiguration<PrintJobType>
    {
        public PrintJobTypeConfiguration()
        {
            ToTable("tbPG_PrintJobType");
            HasKey(t => t.PrintJobTypeId);
            Property(t => t.PrintJobTypeId).HasColumnName("PrintJobTypeID").IsRequired();
            Property(t => t.PrintJobTypeDesc).HasColumnName("PrintJobTypeDesc").HasColumnType("varchar").HasMaxLength(200).IsRequired();

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
