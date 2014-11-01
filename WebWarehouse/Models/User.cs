using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebWarehouse.Models
{
    public class User
    {
        [StringLength(50)]
        public string Address { get; set; }

        [Key]
        public int ID { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole Role { get; set; }

        [Required]
        public string Username { get; set; }
    }
}