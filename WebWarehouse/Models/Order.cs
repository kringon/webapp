using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWarehouse.Models
{

    public enum OrderEnum
    {
        Picked,Ordered,Payed,Shipped,Delivered
    }
    public class Order
    {

        public int ID { get; set; }
        public OrderEnum status { get; set; }
        public DateTime ordered { get; set; }
        public DateTime delivered { get; set; }
     


        public virtual ICollection<Item> Items { get; set; }

        public decimal getTotalPrice(){
            decimal total = 0;
            foreach(var item in Items){
                total += item.Price;
            }

            return total;
        }
    }
}