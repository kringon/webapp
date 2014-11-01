using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebWarehouse.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        public UserRole Role { get; set; }

                
        public virtual ICollection<Order> Orders { get; set; }
    }
}