using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BazarNetCore.Models
{
    public class Clients
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
