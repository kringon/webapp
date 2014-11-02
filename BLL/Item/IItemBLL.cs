namespace WebWarehouse.BLL
{
    public interface IItemBLL
    {
        bool Create(global::WebWarehouse.Model.Item Item);

        bool Delete(int id);

        global::WebWarehouse.Model.Item Find(int id);

        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.Item> FindAll();

        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.Item> ListByCategory(int ItemCategoryID);

        bool Update(global::WebWarehouse.Model.Item Item);
    }
}