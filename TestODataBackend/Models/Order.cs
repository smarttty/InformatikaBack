using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TestODataBackend.Models
{
    public partial class Order
    {
        [Key]
        public Guid _Primarykey { get; set; }
        public string Name { get; set; }
        public Guid UserPrimarykey { get; set; }

        public virtual User User { get; set; }
    }
}
