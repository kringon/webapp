using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WebWarehouse.Models;

namespace WebWarehouse.DAL
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext()
            : base("WarehouseContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemCategory> ItemCategorys { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}