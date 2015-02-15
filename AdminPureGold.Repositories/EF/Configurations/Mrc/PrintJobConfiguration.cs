using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PrintJobConfiguration : EntityTypeConfiguration<PrintJob>
    {
        public PrintJobConfiguration()
        {
            ToTable("tbPG_PrintJob");
            HasKey(t => t.PrintJobId);
            Property(t => t.PrintJobId).HasColumnName("PrintJobID").IsRequired();
            Property(t => t.PrintJobDesc).HasColumnName("PrintJobDesc").HasColumnType("varchar").HasMaxLength(1000).IsOptional();
            Property(t => t.StartDate).HasColumnName("StartDate").IsOptional();
            Property(t => t.EndDate).HasColumnName("EndDate").IsOptional();
            Property(t => t.PrintJobTypeId).HasColumnName("PrintJobTypeID").IsRequired();
            Property(t => t.PrintJobStatusId).HasColumnName("PrintJobStatusID").IsRequired();
            Property(t => t.CrtBy).HasColumnName("CRT_BY").IsOptional();
            Property(t => t.CrtDt).HasColumnName("CRT_DT").IsRequired();
            Property(t => t.UpdBy).HasColumnName("UPD_BY").IsOptional();
            Property(t => t.UpdDt).HasColumnName("UPD_DT").IsOptional();
            
            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }
}
