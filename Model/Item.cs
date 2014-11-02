using System.Collections.Generic;

namespace WebWarehouse.Model
{
    public enum ItemEnum
    {
        Active, Inactive
    }

    public class Item
    {
        public int ID { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }

        public int ItemCategoryID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public decimal Price { get; set; }

        public ItemEnum Status { get; set; }
    }
}