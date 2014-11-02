using System;
using System.Collections.Generic;
using WebWarehouse.DAL;
using WebWarehouse.Model;

namespace WebWarehouse.BLL
{
    public class OrderBLL : IOrderBLL
    {
        private IOrderRepository db;

        public OrderBLL()
        {
            db = new OrderRepository();
        }

        public OrderBLL(IOrderRepository stub)
        {
            db = stub;
        }

        public Dictionary<String, String> addItem(int itemid, int userid)
        {
            return db.addItem(itemid, userid);
        }

        public bool Create(Order order)
        {
            return db.Create(order);
        }

        public bool Delete(int id)
        {
            return db.Delete(id);
        }

        public Order Find(int id)
        {
            return db.Find(id);
        }

        public ICollection<Order> FindAll()
        {
            return db.FindAll();
        }

        public Dictionary<String, String> removeItem(int itemid, int userid)
        {
            return db.removeItem(itemid, userid);
        }

        public bool Update(Order order)
        {
            return db.Update(order);
        }
    }
}