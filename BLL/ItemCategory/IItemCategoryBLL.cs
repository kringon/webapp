namespace WebWarehouse.BLL
{
    public interface IItemCategoryBLL
    {
        bool Create(WebWarehouse.Model.ItemCategory innItemCategory);

        bool Delete(int id);

        WebWarehouse.Model.ItemCategory Find(int id);

        System.Collections.Generic.ICollection<WebWarehouse.Model.ItemCategory> FindAll();

        bool Update(WebWarehouse.Model.ItemCategory itemCategory);
    }
}