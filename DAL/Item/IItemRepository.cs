using System;
namespace WebWarehouse.DAL
{
   public interface IItemRepository
    {
        bool Create(global::WebWarehouse.Model.Item Item);
        bool Delete(int Id);
        global::WebWarehouse.Model.Item Find(int Id);
        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.Item> FindAll();
        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.Item> ListByCategory(int ItemCategoryID);
        bool Update(global::WebWarehouse.Model.Item Item);
    }
}
