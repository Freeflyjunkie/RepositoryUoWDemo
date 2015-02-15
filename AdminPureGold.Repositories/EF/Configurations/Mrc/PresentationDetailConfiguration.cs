
using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class PresentationDetailConfiguration: EntityTypeConfiguration<PresentationDetail>
    {
        public PresentationDetailConfiguration()
        {
            ToTable("tbPresentationDetail");
            HasKey(t => t.TransactionId);            
            Property(t => t.TransactionId).HasColumnName("TransactionID").IsRequired();
            Property(t => t.PresentationDate).IsOptional();
            Property(t => t.CustomerName).HasColumnName("CustomerName").HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(t => t.LeaveBehindLetterName).HasColumnName("LBLetterName").HasColumnType("varchar").HasMaxLength(150).IsOptional();
            Property(t => t.CrtBy).HasColumnName("CRT_BY").IsRequired();
            Property(t => t.CrtDt).HasColumnName("CRT_DT").IsRequired();
            Property(t => t.UpdBy).HasColumnName("UPD_BY").IsOptional();
            Property(t => t.UpdDt).HasColumnName("UPD_DT").IsOptional();
            Ignore(t => t.EntityStateForGraphsUpdates);                       
        }
    }
}
