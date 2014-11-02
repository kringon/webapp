using System;
using System.Collections.Generic;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class WareHouseInitializer : System.Data.Entity.DropCreateDatabaseAlways<WarehouseContext>
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
                new Item{ItemCategoryID=1, Name="Jakke",Price=299.99m},

                
                new Item{ItemCategoryID=3, Name="Rødvin",Price=100m},
                new Item{ItemCategoryID=3, Name="Hvitvin",Price=200m},
                new Item{ItemCategoryID=3, Name="Rosévin",Price=350m},

                new Item{ItemCategoryID=2, Name="Mazda",Price=350546m},
                new Item{ItemCategoryID=2, Name="Kia",Price=251546m},
                new Item{ItemCategoryID=2, Name="Toyota",Price=387500m},
                new Item{ItemCategoryID=2, Name="Opel",Price=245000m},

                new Item{ItemCategoryID=4, Name="Rød",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Grønn",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Blå",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Gul",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Brun",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Sort",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Hvit",Price=189.5m},

                new Item{ItemCategoryID=5, Name="Pensko",Price=150m},
                new Item{ItemCategoryID=5, Name="Støvler",Price=250m},
                new Item{ItemCategoryID=5, Name="Joggesko",Price=349m},
                new Item{ItemCategoryID=5, Name="Tøfler",Price=199m},

                new Item{ItemCategoryID=6, Name="Mobiltelefon",Price=2948m},
                new Item{ItemCategoryID=6, Name="Tannbørste",Price=123m},

                new Item{ItemCategoryID=7, Name="Gitar",Price=2478},
                new Item{ItemCategoryID=7, Name="CD",Price=159m}
            };
            items.ForEach(i => context.Items.Add(i));
            context.SaveChanges();
            var numberOfItems = new List<ItemQuantity>();

            items.ForEach(i => numberOfItems.Add(new ItemQuantity() { Value=1, Item=i}));
            
            
            var orders = new List<Order>{
                new Order{Ordered=DateTime.Parse("2014-04-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered, Items=items,ItemQuantities=numberOfItems}
            };
            var users = new List<User>
            {
                new User{Username="sdfg",Password=hash("sdfg"),Address="sdfg",  Role = UserRole.Admin , Orders = orders},
                new User{Username="wert",Password=hash("wert"),Address="wert", Role = UserRole.Customer},
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