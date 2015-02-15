using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.WeichertSL;

namespace AdminPureGold.Repositories.EF.Configurations.WeichertSL
{
    class ListToAssociateConfiguration : EntityTypeConfiguration<ListToAssociate>
    {
        public ListToAssociateConfiguration()
        {
            ToTable("tbListToAssociate");
            HasKey(t => t.ListToAssociateId);
            Property(t => t.ListToAssociateId).HasColumnName("ListToAssociateID").IsRequired();

            Property(t => t.ListId).HasColumnName("ListID").IsRequired();
            Property(t => t.ListAssociateNumber)
                .HasColumnName("ListAssocNo")
                .HasColumnType("char")
                .HasMaxLength(5)
                .IsRequired();
            Property(t => t.AsscociateType)
                .HasColumnName("AssocType")
                .HasColumnType("char")
                .HasMaxLength(2)
                .IsRequired();
            Property(t => t.ListAssociateOfficeId).HasColumnName("ListAssocOfficeID").IsOptional();
            Property(t => t.PersonNumber).HasColumnName("WPersno").IsOptional();
            Property(t => t.RelationshipNumber).HasColumnName("WRelateNo").IsOptional();
        }
    }
}
