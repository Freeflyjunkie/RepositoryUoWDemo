using System.Data.Entity;
using System.Diagnostics;
using AdminPureGold.Repositories.EF.Configurations.WeicherCore;
using AdminPureGold.Domain.Models.WeichertCore;

namespace AdminPureGold.Repositories.EF
{
    public class WeichertCoreContext : DbContext
    {
        public WeichertCoreContext()
            : base("WeichertCoreContext")
        {
            Database.SetInitializer<WeichertCoreContext>(null);
            Database.Log = sql => Debug.Write(sql);
        }    

        public DbSet<Office> Offices { get; set; }        
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonToRelate> PersonToRelates { get; set; }
        public DbSet<RelateToEmail> RelateToEmails { get; set; }
        public DbSet<RelateToName> RelateToNames { get; set; }
        public DbSet<RelateToPhone> RelateToPhones { get; set; }
        public DbSet<PersonToImage> PersonToImages  { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Configurations.Add(new OfficeConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new PersonToRelateConfiguration());
            modelBuilder.Configurations.Add(new RelateToAddressConfiguration());
            modelBuilder.Configurations.Add(new RelateToEmailConfiguration());
            modelBuilder.Configurations.Add(new RelateToNameConfiguration());
            modelBuilder.Configurations.Add(new RelateToPhoneConfiguration());
            modelBuilder.Configurations.Add(new PersonToImageConfiguration());
        }
    }
}
