using System;
using System.Collections.Generic;
using WebWarehouse.DAL;
using WebWarehouse.Model;

namespace WebWarehouse.BLL
{
    public class OrderBLL
    {
        private OrderDAL db = new OrderDAL();

        public Dictionary<String, String> addItem(int? itemid, int? userid)
        {
            return db.addItem(itemid, userid);
        }

        public bool Create(Order Order)
        {
            return db.Create(Order);
        }

        public bool Delete(int id)
        {
            return db.Delete(id);
        }

        public Order Find(int? id)
        {
            return db.Find(id);
        }

        public ICollection<Order> FindAll()
        {
            return db.FindAll();
        }

        public Dictionary<String, String> removeItem(int? itemid, int? userid)
        {
            return db.removeItem(itemid, userid);
        }

        public bool Update(Order Order)
        {
            return db.Update(Order);
        }
    }
}