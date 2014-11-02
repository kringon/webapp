using System;
namespace WebWarehouse.DAL
{
    public interface IItemCategoryRepository
    {
        bool Create(global::WebWarehouse.Model.ItemCategory ItemCategory);
        bool Delete(int Id);
        global::WebWarehouse.Model.ItemCategory Find(int Id);
        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.ItemCategory> FindAll();
        bool Update(global::WebWarehouse.Model.ItemCategory ItemCategory);
    }
}
