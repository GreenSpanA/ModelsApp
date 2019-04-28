using ModelsApp.Models;
using System.Collections.Generic;

namespace ModelsApp.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Menu> Menus { get; set; }
        public IEnumerable<FileModel> Files { get; set; }
    }
}
