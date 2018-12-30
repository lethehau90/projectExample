using System.Data.Entity;
using App.Model.Entity;

namespace App.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext() : base("AppConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ItemMaster> ItemMasterS { set; get; }
        public DbSet<ItemMasterInventory> ItemMasterInventorys { set; get; }

        public DbSet<Site> Sites { set; get; }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
           
        }
    }
}
