using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class ItemRepository : IItemRepository
    {
        private WarehouseContext Db = new WarehouseContext();
        private ILog Logger = LogManager.GetLogger(typeof(ItemRepository));

        //Create a new Item from a bound item
        public bool Create(Item Item)
        {
            try
            {
                Db.Items.Add(Item);
                Db.SaveChanges();
                Logger.Info("Added new Item");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Delete an Item by ID
        public bool Delete(int Id)
        {
            try
            {
                Item Item = Db.Items.Find(Id);
                Db.Items.Remove(Item);
                Db.SaveChanges();
                Logger.Info("Item with id: " + Id + " was deleted.");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Find an ItemCategory by ID
        public Item Find(int Id)
        {
            Item Found;
            try
            {
                Logger.Info("Searching for Item with id: " + Id);
                Found = Db.Items.Find(Id);
                return Found;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public ICollection<Item> FindAll()
        {
            try
            {
                return Db.Items.Include(i => i.ItemCategory).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public ICollection<Item> ListByCategory(int ItemCategoryID)
        {
            try
            {
                return Db.Items.Where(x => x.ItemCategoryID == ItemCategoryID).ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
            
        }

        //Update a specific ItemCategory
        public bool Update(Item Item)
        {
            try
            {
                Db.Entry(Item).State = EntityState.Modified;
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