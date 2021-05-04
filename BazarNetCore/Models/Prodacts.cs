using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BazarNetCore.Models
{
    public class Prodacts
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Salers { get; set; }
        public string ProdactType { get; set; }
        public string Weight { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeTypeOfData { get; set; }
    }
}
