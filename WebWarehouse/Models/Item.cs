using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebWarehouse.Models
{
    public class Item
    {
        public int ID { get; set; }
        public int ItemCategoryID { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
    }
}