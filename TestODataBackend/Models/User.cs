using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestODataBackend.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public Guid _Primarykey { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
