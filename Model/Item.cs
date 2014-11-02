using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ItemEnum Status { get; set; }
    }
}