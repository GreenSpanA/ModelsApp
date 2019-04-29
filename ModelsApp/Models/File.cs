using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsApp.Models
{
    public class File : BaseEntity
    {
        public int Id { get; set; }
        public int Name { get; set; }     
        public bool is_Curremt { get; set; }
    }
}
