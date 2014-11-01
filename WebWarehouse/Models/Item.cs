using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWarehouse.Models
{
       public enum ItemEnum
    {
        Active, Inactive
    }

    [Serializable()]
    public class Item
    {
        public int ID { get; set; }
        public int ItemCategoryID { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public ItemEnum Status { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}