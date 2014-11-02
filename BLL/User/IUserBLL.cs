using System;
namespace WebWarehouse.BLL
{
    public interface IUserBLL
    {
        bool Create(global::WebWarehouse.Model.User user);
        bool Delete(int id);
        global::WebWarehouse.Model.User existingUser(global::WebWarehouse.Model.User user);
        global::WebWarehouse.Model.User Find(int id);
        global::System.Collections.Generic.ICollection<global::WebWarehouse.Model.User> FindAll();
        global::WebWarehouse.Model.Order getFirstOrderByStatus(int userID, global::WebWarehouse.Model.OrderEnum orderEnum);
        bool Update(global::WebWarehouse.Model.User user);
        global::WebWarehouse.Model.Order ActiveOrder(int userId);
    }
}
