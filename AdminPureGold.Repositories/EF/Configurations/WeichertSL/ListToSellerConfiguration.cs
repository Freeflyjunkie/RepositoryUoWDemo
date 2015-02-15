using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class ListToSellerConfiguration : EntityTypeConfiguration<ListToSeller>
    {
        public ListToSellerConfiguration()
        {
            ToTable("tbListToSeller");
            HasKey(p => p.ListToSellerId);
            Property(p => p.ListToSellerId).HasColumnName("ListToSellerID").IsRequired();

            Property(p => p.ListId).HasColumnName("ListID").IsRequired();
            Property(p => p.SellerSequenceNumber).HasColumnName("SellerSeqNo").IsRequired();
            Property(p => p.Title).HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(p => p.FirstName).HasColumnName("FName").HasColumnType("varchar").HasMaxLength(35).IsOptional();
            Property(p => p.MiddleName).HasColumnName("MName").HasColumnType("varchar").HasMaxLength(35).IsOptional();
            Property(p => p.LastName).HasColumnName("LName").HasColumnType("varchar").HasMaxLength(35).IsOptional();
            Property(p => p.Address1).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.Address2).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.City).HasColumnType("varchar").HasMaxLength(50).IsOptional();
            Property(p => p.State).HasColumnType("char").HasMaxLength(2).IsOptional();
            Property(p => p.Zip).HasColumnType("varchar").HasMaxLength(15).IsOptional();
            Property(p => p.HomePhone).HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(p => p.BusinessPhone).HasColumnName("BusPhone").HasColumnType("varchar").HasMaxLength(10).IsOptional();
            Property(g => g.CrUser).HasColumnName("CRUSER").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            Property(g => g.CrDate).HasColumnName("CRDATE").IsRequired();
            Property(g => g.ChUser).HasColumnName("CHUSER").HasColumnType("varchar").HasMaxLength(25).IsOptional();
            Property(g => g.ChDate).HasColumnName("CHDATE").IsOptional();
        }
    }
}
