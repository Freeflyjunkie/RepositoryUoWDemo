using System.Data.Entity;
using System.Diagnostics;
using AdminPureGold.Domain.Models.WeichertSL;
using AdminPureGold.Repositories.EF.Configurations.WeichertSL;

namespace AdminPureGold.Repositories.EF
{
    public class WeichertSLContext : DbContext
    {
        public WeichertSLContext()
            : base("WeichertSLContext")
        {
            Database.SetInitializer<WeichertSLContext>(null);
            Database.Log = sql => Debug.Write(sql);
        }    

        public DbSet<List> Lists { get; set; }
        public DbSet<ListProperty> ListProperties { get; set; }
        public DbSet<ListToAssociate> ListToAssociates { get; set; }
        public DbSet<ListToSeller> ListToSellers { get; set; }
        public DbSet<ListType> ListTypes { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleToAssociate> SaleToAssociates { get; set; }
        public DbSet<PureGoldSale> PureGoldSales { get; set; }
        public DbSet<SaleToBroker> SaleToBrokers { get; set; }
        public DbSet<SaleToBuyer> SaleToBuyers { get; set; }        
        public DbSet<SaleType> SaleTypes { get; set; }
        public DbSet<BuyerGreeting> BuyerGeetings { get; set; }
        public DbSet<Closing> Closings { get; set; }                
        public DbSet<SourceCd> SourceCds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ListConfiguration());
            modelBuilder.Configurations.Add(new ListPropertyConfiguration());
            modelBuilder.Configurations.Add(new ListToAssociateConfiguration());            
            modelBuilder.Configurations.Add(new ListToSellerConfiguration());
            modelBuilder.Configurations.Add(new ListTypeConfiguration());

            modelBuilder.Configurations.Add(new SaleConfiguration());
            modelBuilder.Configurations.Add(new SaleToAssociateConfiguration());
            modelBuilder.Configurations.Add(new PureGoldSaleConfiguration());                        
            modelBuilder.Configurations.Add(new SaleTypeConfiguration());
            modelBuilder.Configurations.Add(new BuyerGreetingConfiguration());
            modelBuilder.Configurations.Add(new ClosingConfiguration());      
            modelBuilder.Configurations.Add(new SaleToBrokerConfiguration());
            modelBuilder.Configurations.Add(new SaleToBuyerConfiguration());            
            
            modelBuilder.Configurations.Add(new SourceCdConfiguration());
        }
    }
}
