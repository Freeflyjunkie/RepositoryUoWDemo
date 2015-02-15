using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class SaleToBrokerConfiguration : EntityTypeConfiguration<SaleToBroker>
    {
        public SaleToBrokerConfiguration()
        {
            ToTable("tbSaleToBroker");
            HasKey(p => p.SaleToBrokerId);
            Property(p => p.SaleToBrokerId).HasColumnName("SaleToBrokerID").IsRequired();
            Property(p => p.SaleId).HasColumnName("SaleID").IsRequired();
            Property(p => p.SaleBrokerSequenceNumber).HasColumnName("SaleBrokerSeqNo").IsRequired();
            Property(p => p.SaleBrokerNumber).HasColumnType("varchar").HasMaxLength(15).IsRequired();
        }
    }
}
