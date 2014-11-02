using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class ItemCategoryRepository : DAL.IItemCategoryRepository
    {
        private WarehouseContext Db = new WarehouseContext();
        private ILog Logger = LogManager.GetLogger(typeof(ItemCategoryRepository));

        //Create a new Item Category from a bound item
        public bool Create(ItemCategory ItemCategory)
        {
            try
            {
                Db.ItemCategorys.Add(ItemCategory);
                Db.SaveChanges();
                Logger.Info("Added new ItemCategory");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Delete an Item Category by ID
        public bool Delete(int Id)
        {
            try
            {
                ItemCategory ItemCategory = Db.ItemCategorys.Find(Id);
                Db.ItemCategorys.Remove(ItemCategory);
                Db.SaveChanges();
                Logger.Info("ItemCategory with id: " + Id + " was deleted.");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Find an ItemCategory by ID
        public ItemCategory Find(int Id)
        {
            ItemCategory Found;
            try
            {
                Logger.Info("Searching for ItemCategory with id: " + Id);
                Found = Db.ItemCategorys.Find(Id);
                return Found;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public ICollection<ItemCategory> FindAll()
        {
            try
            {
                return Db.ItemCategorys.ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        //Update a specific ItemCategory
        public bool Update(ItemCategory ItemCategory)
        {
            try
            {
                Db.Entry(ItemCategory).State = EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }
    }
}