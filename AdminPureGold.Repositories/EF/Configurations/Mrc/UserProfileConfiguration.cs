using System.Data.Entity.ModelConfiguration;
using AdminPureGold.Domain.Models.Mrc;

namespace AdminPureGold.Repositories.EF.Configurations.Mrc
{
    class UserProfileConfiguration : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            ToTable("tbProfileUser");
            HasKey(t => t.ProfileUserId);
            Property(t => t.ProfileUserId).HasColumnName("ProfileUserID").IsRequired();
            Property(t => t.PersonNumber).HasColumnName("WPersNo").IsRequired();
            Property(t => t.RelationshipNumber).HasColumnName("WRelateNo");
            Property(t => t.XWrNetId).HasColumnName("x_WRNetID");
            Property(t => t.XAscNum).HasColumnName("x_AscNum");
            Property(t => t.RelateToNameId).HasColumnName("RelateToNameID");
            Property(t => t.RelateToPhoneId).HasColumnName("RelateToPhoneID");
            Property(t => t.RelateToEmailId).HasColumnName("RelateToEmailID");
            Property(t => t.AssociateTitleId).HasColumnName("AssociateTitleID");
            Property(t => t.ShowMiddleInitial).IsRequired();            
            Property(t => t.ShowCommonName).IsRequired();
            Property(t => t.ShowTitle).IsRequired();
            Property(t => t.ShowSuffix).IsRequired();
            Property(t => t.ShowPhone).IsRequired();
            Property(t => t.ShowEmail).IsRequired();
            Property(t => t.ShowWebsite).IsRequired();
            Property(t => t.CrtBy).HasColumnName("CRT_BY");
            Property(t => t.CrtDt).HasColumnName("CRT_DT");
            Property(t => t.UpdBy).HasColumnName("UPD_BY");
            Property(t => t.UpdDt).HasColumnName("UPD_DT");

            Ignore(t => t.EntityStateForGraphsUpdates);
        }
    }    
}
