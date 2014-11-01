using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebWarehouse.Models
{

    public enum OrderEnum
    {
        Empty,Browsing,Ordered,Payed,Shipped,Delivered
    }
    public class Order
    {
        [Key]
        public int ID { get; set; }
       
        public OrderEnum Status { get; set; }

        public DateTime? Ordered { get; set; }
        public DateTime? Delivered { get; set; }
        
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