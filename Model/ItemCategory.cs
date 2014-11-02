using System.ComponentModel.DataAnnotations;

namespace WebWarehouse.Model
{
    public class ItemCategory
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}