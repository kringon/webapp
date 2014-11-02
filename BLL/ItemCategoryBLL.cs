using System.Collections.Generic;
using WebWarehouse.DAL;
using WebWarehouse.Model;

namespace WebWarehouse.BLL
{
    public class ItemCategoryBLL
    {
        private ItemCategoryDAL db = new ItemCategoryDAL();

        public bool Create(ItemCategory innItemCategory)
        {
            return db.Create(innItemCategory);
        }

        public bool Delete(int id)
        {
            return db.Delete(id);
        }

        public ItemCategory Find(int? id)
        {
            return db.Find(id);
        }

        public ICollection<ItemCategory> FindAll()
        {
            return db.FindAll();
        }

        public bool Update(ItemCategory itemCategory)
        {
            return db.Update(itemCategory);
        }
    }
}