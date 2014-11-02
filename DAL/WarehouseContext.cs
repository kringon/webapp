using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext()
            : base("WarehouseContext")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<ItemCategory> ItemCategorys { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}