using System.Data.Entity;
using System.Diagnostics;
using AdminPureGold.Domain.Models.AtlasX;
using AdminPureGold.Repositories.EF.Configurations.AtlasX;

namespace AdminPureGold.Repositories.EF
{
    public class AtlasXContext : DbContext
    {
        public AtlasXContext()
            : base("AtlasXContext")
        {
            Database.SetInitializer<AtlasXContext>(null);
            Database.Log = sql => Debug.Write(sql);
        }        

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyAlternate> PropertyAlternates { get; set; }
        public DbSet<WApplication> Applications { get; set; }
        public DbSet<WAtlasX> AtlasXs { get; set; }
        public DbSet<WAtlasXToApp> AtlasXToApps { get; set; }
        public DbSet<WAtlasXtoAppRole> AtlasXtoAppRoles { get; set; }
        public DbSet<WAtlasXtoAppWPerson> AtlasXtoAppWPersons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PropertyConfiguration());
            modelBuilder.Configurations.Add(new PropertyAlternateConfiguration());

            modelBuilder.Configurations.Add(new WApplicationConfiguration());
            modelBuilder.Configurations.Add(new WAtlasXConfiguration());
            modelBuilder.Configurations.Add(new WAtlasXToAppConfiguration());
            modelBuilder.Configurations.Add(new WAtlasXtoAppRoleConfiguration());
            modelBuilder.Configurations.Add(new WAtlasXtoAppWPersonConfiguration());
        }
    }
}
