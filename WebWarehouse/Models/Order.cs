using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebWarehouse.Models
{
    public enum OrderEnum
    {
        Empty, Browsing, Ordered, Payed, Shipped, Delivered
    }

    public class Order
    {
        public DateTime? Delivered { get; set; }

        [Key]
        public int ID { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public DateTime? Ordered { get; set; }

        public OrderEnum Status { get; set; }

        public decimal getTotalPrice()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Price;
            }

            return total;
        }
    }
}