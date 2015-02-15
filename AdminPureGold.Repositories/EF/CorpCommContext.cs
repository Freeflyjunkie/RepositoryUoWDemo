
using System.Data.Entity;
using AdminPureGold.Domain.Models.CorpComm;
using AdminPureGold.Repositories.EF.Configurations.CorpComm;
using System.Diagnostics;

namespace AdminPureGold.Repositories.EF
{
    public class CorpCommContext : DbContext
    {
        public CorpCommContext()
            : base("CorpCommContext")
        {
            Database.SetInitializer<CorpCommContext>(null);
            Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<McMessage> McMessage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new McMessageConfiguration());
        }

    }
}
