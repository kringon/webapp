using System;
using System.Collections.Generic;

namespace WebWarehouse.Models
{
    
    public class ItemQuantity
    {
        public int ID { get; set; }

        public int Value { get; set; }

        public virtual Item Item { get; set; }

        public decimal getTotalValue(){
            return Value * Item.Price;
        }
    }
}