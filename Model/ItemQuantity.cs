namespace WebWarehouse.Model
{
    public class ItemQuantity
    {
        public int ID { get; set; }

        public virtual Item Item { get; set; }

        public int Value { get; set; }

        public decimal getTotalValue()
        {
            return Value * Item.Price;
        }
    }
}