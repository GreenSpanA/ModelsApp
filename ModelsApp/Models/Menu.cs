using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsApp.Models
{
    public class Menu : BaseEntity
    {
        
        public int Id { get; set; }
       
        public string Category { get; set; }
       
        public string Dish { get; set; }
        
        public string Description { get; set; }
       
        public string Veg_Comment { get; set; }
       
        public string Price { get; set; }
        
        public string File_Name { get; set; }
    }
}
