using System;
namespace WebWarehouse.BLL
{
    public interface IOrderBLL
    {
        global::System.Collections.Generic.Dictionary<string, string> addItem(int itemid, int userid);
        bool Create(global::WebWarehouse.Model.Order order);
        bool Delete(int id);
        global::WebWarehouse.Model.Order Find(int id);
        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.Order> FindAll();
        global::System.Collections.Generic.Dictionary<string, string> removeItem(int itemid, int userid);
        bool Update(global::WebWarehouse.Model.Order order);
    }
}
