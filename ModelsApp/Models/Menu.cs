using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsApp.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public File Manufacturer { get; set; }
        public decimal Price { get; set; }
    }
}
