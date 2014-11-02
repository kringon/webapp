using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class OrderDAL
    {
        private WarehouseContext Db = new WarehouseContext();
        private ILog Logger = LogManager.GetLogger(typeof(OrderDAL));

        public Dictionary<String, String> addItem(int? itemid, int? userid)
        {
            Dictionary<String, String> returnType = new Dictionary<string, string>();

            try
            {
                Item item = Db.Items.Find(itemid);
                User user = Db.Users.Find(userid);
                Order order = user.Orders.FirstOrDefault(o => o.Status == OrderEnum.Browsing || o.Status == OrderEnum.Empty);

                if (order == null)
                {
                    order = new Order();
                    order.Status = OrderEnum.Empty;

                    order.Items = new List<Item>();
                    order.ItemQuantities = new List<ItemQuantity>();
                    user.Orders.Add(order);
                    Db.Entry(user).State = EntityState.Modified;
                    Db.SaveChanges();
                }

                order.Items.Add(item);
                ItemQuantity quantity = order.getItemQuantity(item);
                if (quantity != null)
                    quantity.Value++;
                else
                    order.ItemQuantities.Add(new ItemQuantity() { Value = 1, Item = item });

                if (order.Status == OrderEnum.Empty)
                    order.Status = OrderEnum.Browsing;

                Db.Entry(order).State = EntityState.Modified;
                Db.Entry(user).State = EntityState.Modified;

                Db.SaveChanges();
                Logger.Info("Item with ID: " + itemid + " was successfully added to order for userID: " + userid);

                returnType.Add("TotalPrice", order.getTotalPrice().ToString());
                returnType.Add("ItemName", item.Name);
                returnType.Add("error", "none");
                return returnType;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                returnType.Add("error", "e.getMessage");
                return returnType;
            }
        }

        //Create a new Order from a bound Order
        public bool Create(Order Order)
        {
            try
            {
                Db.Orders.Add(Order);
                Db.SaveChanges();
                Logger.Info("Added new Order");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Delete an Order by ID
        public bool Delete(int Id)
        {
            try
            {
                Order Order = Db.Orders.Find(Id);
                Db.Orders.Remove(Order);
                Db.SaveChanges();
                Logger.Info("Order with id: " + Id + " was deleted.");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }

        //Find an OrderCategory by ID
        public Order Find(int? Id)
        {
            Order Found;
            try
            {
                Logger.Info("Searching for Order with id: " + Id);
                Found = Db.Orders.Find(Id);
                return Found;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public ICollection<Order> FindAll()
        {
            try
            {
                return Db.Orders.ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return null;
            }
        }

        public Dictionary<String, String> removeItem(int? itemid, int? userid)
        {
            Dictionary<String, String> returnType = new Dictionary<string, string>();

            try
            {
                Item item = Db.Items.Find(itemid);
                User user = Db.Users.Find(userid);
                Order order = user.Orders.FirstOrDefault(o => o.Status == OrderEnum.Browsing || o.Status == OrderEnum.Empty);

                if (order == null)
                {
                    returnType.Add("error", "Something Went Wrong during removal. Please contact administrator. ItemID:" + itemid + " UserID:" + userid);
                    return returnType;
                }

                order.Items.Remove(item);
                order.ItemQuantities.Remove(order.getItemQuantity(item));

                if (order.Items.Count == 0)
                    order.Status = OrderEnum.Empty;

                Db.Entry(order).State = EntityState.Modified;
                Db.Entry(user).State = EntityState.Modified;
                Db.SaveChanges();

                returnType.Add("error", "none");
                returnType.Add("TotalPrice", order.getTotalPrice().ToString());
                returnType.Add("itemID", item.ID.ToString());

                return returnType;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                returnType.Add("error", "e.getMessage");
                return returnType;
            }

            return null;
        }

        //Update a specific OrderCategory
        public bool Update(Order Order)
        {
            try
            {
                Db.Entry(Order).State = EntityState.Modified;
                Db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }
    }
}