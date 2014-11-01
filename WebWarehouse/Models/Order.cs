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
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Delivered { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? Ordered { get; set; }

        [Key]
        public int ID { get; set; }

        public virtual User user { get; set; }

        public virtual ICollection<Item> Items { get; set; }

        public virtual ICollection<ItemQuantity> ItemQuantities { get; set; }

        public OrderEnum Status { get; set; }

        public decimal getTotalPrice()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                int value = getItemQuantity(item).Value;
                total += item.Price * value;
            }

            return total;
        }

        public ItemQuantity getItemQuantity(Item item)
        {
            foreach (ItemQuantity itemQuantity in ItemQuantities)
            {
                if (itemQuantity.Item.Equals(item))
                {
                    return itemQuantity;
                }
            }
            return null;
        }
    }
}