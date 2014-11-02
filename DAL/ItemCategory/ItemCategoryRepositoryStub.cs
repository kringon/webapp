using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class ItemCategoryRepositoryStub : DAL.IItemCategoryRepository
    {
        public bool Create(ItemCategory itemCategory)
        {
            if (itemCategory.ID == 0)
                return false;
            else
                return true;
        }

        //Delete an Item Category by ID
        public bool Delete(int id)
        {
            if (id == 0)
                return false;
            else
                return true;
        }

        //Find an ItemCategory by ID
        public ItemCategory Find(int Id)
        {
            if (Id == null || Id == 0)
            {
                return null;
            }
            else
            {
                ItemCategory itemCategory = new ItemCategory()
                {
                    ID = 1,
                    Name = "Klær"
                };

                return itemCategory;
            }
        }

        public ICollection<ItemCategory> FindAll()
        {
            List<ItemCategory> itemCategoryList = new List<ItemCategory>()
            {
                new ItemCategory{Name="Klær"},
                new ItemCategory{Name="Biler"},
                new ItemCategory{Name="Vin"},
                new ItemCategory{Name="Maling"},
                new ItemCategory{Name="Sko"},
                new ItemCategory{Name="Elektronikk"},
                new ItemCategory{Name="Musikk"},
            };

            return itemCategoryList;
        }

        
        public bool Update(ItemCategory itemCategory)
        {
            if (itemCategory.ID == 0)
                return false;
            else
                return true;
        }
    }
}