using System;
using System.Collections.Generic;
using WebWarehouse.Model;

namespace WebWarehouse.DAL
{
    public class OrderRepositoryStub : IOrderRepository
    {
        public Dictionary<String, String> addItem(int itemid, int userid)
        {
            Dictionary<String, String> returnType = new Dictionary<string, string>();

            if (itemid == 0 || userid == 0)
            {
                returnType.Add("error", "Failed while inserting new item");
                return returnType;
            }
            else
            {
                returnType.Add("TotalPrice", "299");
                returnType.Add("ItemName", "Bukse");
                returnType.Add("error", "none");
                return returnType;
            }
        }

        //Create a new Order from a bound Order
        public bool Create(Order order)
        {
            if (order.ID == 0)
                return false;
            else
                return true;
        }

        //Delete an Order by ID
        public bool Delete(int id)
        {
            if (id == 0)
                return false;
            else
                return true;
        }

        //Find an OrderCategory by ID
        public Order Find(int id)
        {
            if (id == 0)
            {
                return null;
            }
            else
            {
                Order order = new Order()
                {
                    ID = id,
                };

                return order;
            }
        }

        public ICollection<Order> FindAll()
        {
            List<Order> orderList = new List<Order>{
                new Order{Ordered=DateTime.Parse("2014-04-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered},
                new Order{Ordered=DateTime.Parse("2014-03-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered},
                new Order{Ordered=DateTime.Parse("2014-02-04"), Delivered=DateTime.Parse("2014-05-05"),Status=OrderEnum.Ordered}
            };

            return orderList;
        }

        public Dictionary<String, String> removeItem(int itemid, int userid)
        {
            Dictionary<String, String> returnType = new Dictionary<string, string>();

            if (itemid == 0 || userid == 0)
            {
                returnType.Add("error", "Something Went Wrong during removal. Please contact administrator. ItemID:" + itemid + " UserID:" + userid);
                return returnType;
            }
            else
            {
                returnType.Add("error", "none");
                returnType.Add("TotalPrice", "990");
                returnType.Add("itemID", itemid.ToString());
                return returnType;
            }
        }

        //Update a specific OrderCategory
        public bool Update(Order order)
        {
            if (order.ID == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}