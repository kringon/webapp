using System.Collections.Generic;
using WebWarehouse.DAL;
using WebWarehouse.Model;

namespace WebWarehouse.BLL
{
    public class ItemBLL : IItemBLL
    {
        private IItemRepository db;

        public ItemBLL()
        {
            db = new ItemRepository();
        }

        public ItemBLL(IItemRepository stub)
        {
            db = stub;
        }

        public bool Create(Item Item)
        {
            return db.Create(Item);
        }

        public bool Delete(int id)
        {
            return db.Delete(id);
        }

        public Item Find(int id)
        {
            return db.Find(id);
        }

        public ICollection<Item> FindAll()
        {
            return db.FindAll();
        }

        public ICollection<Item> ListByCategory(int ItemCategoryID)
        {
            return db.ListByCategory(ItemCategoryID);
        }

        public bool Update(Item Item)
        {
            return db.Update(Item);
        }
    }
}