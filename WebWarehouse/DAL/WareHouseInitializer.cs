using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebWarehouse.Models;

namespace WebWarehouse.DAL
{
    public class WareHouseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WarehouseContext>
    {
        protected override void Seed(WarehouseContext context)
        {


            var itemCateogories = new List<ItemCategory>{
                new ItemCategory{Name="Klær"},
                new ItemCategory{Name="Biler"},
                new ItemCategory{Name="Vin"},
                new ItemCategory{Name="Maling"},
                new ItemCategory{Name="Sko"},
                new ItemCategory{Name="Elektronikk"},
                new ItemCategory{Name="Musikk"},
            };
            itemCateogories.ForEach(ic => context.ItemCategorys.Add(ic));
            context.SaveChanges();

            var items = new List<Item>{
                new Item{ItemCategoryID=1, Name="Bukse",Price=99.99m},
                new Item{ItemCategoryID=1, Name="Genser",Price=199.99m},
                new Item{ItemCategoryID=1, Name="Jakke",Price=299.99m}
            };
            items.ForEach(i => context.Items.Add(i));
            context.SaveChanges();


            


            var orders = new List<Order>{
                new Order{Ordered=DateTime.Parse("2014-04-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered, Items=items}
            };
            var users = new List<User>
            {
                new User{Username="asdf",Password=hash("asdf"),Address="asdf",  Role = UserRole.Admin , Orders = orders},
                new User{Username="qwer",Password=hash("qwer"),Address="qwer", Role = UserRole.Customer},

            
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();



           




        
        }

        private String hash(string password)
        {
            var algoritme = System.Security.Cryptography.SHA256.Create();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            return Convert.ToBase64String(algoritme.ComputeHash(data));

        }


    }
}