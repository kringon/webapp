using System.Collections.Generic;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class ItemRepositoryStub : IItemRepository
    {
        //Create a new Item from a bound item
        public bool Create(Item item)
        {
            if (item.ID == 0)
                return false;
            else
                return true;
        }

        //Delete an Item by ID
        public bool Delete(int id)
        {
            if (id == 0)
                return false;
            else
                return true;
        }

        //Find an ItemCategory by ID
        public Item Find(int id)
        {
            if ( id == 0)
            {
                return null;
            }
            else
            {
                Item item = new Item()
                {
                    ID = id,
                    Name = "Bukse",
                    Price = 99m,
                    Status = ItemEnum.Active
                };

                return item;
            }
        }

        public ICollection<Item> FindAll()
        {
            List<Item> itemList = new List<Item>()
            {
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
                new Item{ItemCategoryID=7, Name="CD",Price=159m},
            };

            return itemList;
        }

        public ICollection<Item> ListByCategory(int id)
        {
            if (id == 4)
            {
                List<Item> itemCategoryList = new List<Item>(){
                   new Item{ItemCategoryID=4, Name="Rød",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Grønn",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Blå",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Gul",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Brun",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Sort",Price=199.5m},
                new Item{ItemCategoryID=4, Name="Hvit",Price=189.5m},
            };

                return itemCategoryList;
            }
            else
            {
                return null;
            }
        }

        //Update a specific ItemCategory
        public bool Update(Item item)
        {
            if (item.ID == 0)
                return false;
            else
                return true;
        }
    }
}