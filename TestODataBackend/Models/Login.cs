using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestODataBackend.Models
{
    public class Login
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
