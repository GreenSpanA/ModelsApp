using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ModelsApp.Models; // пространство имен моделей
using ModelsApp.ViewModels;

namespace ModelsApp.Controllers
{
    public class HomeController : Controller
    {
        List<File> files;
        List<Menu> menus;

        public HomeController()
        {
            File apple = new File { Id = 1, Name = "Apple", Country = "США" };
            File microsoft = new File { Id = 2, Name = "Microsoft", Country = "США" };
            File google = new File { Id = 3, Name = "Google", Country = "США" };
            files = new List<File> { apple, microsoft, google };

            menus = new List<Menu>
            {
                new Menu { Id=1, Manufacturer= apple, Name="iPhone 6S", Price=56000 },
                new Menu { Id=2, Manufacturer= apple, Name="iPhone 5S", Price=41000 },
                new Menu { Id=3, Manufacturer= microsoft, Name="Lumia 550", Price=9000 },
                new Menu { Id=4, Manufacturer= microsoft, Name="Lumia 950", Price=40000 },
                new Menu { Id=5, Manufacturer= google, Name="Nexus 5X", Price=30000 },
                new Menu { Id=6, Manufacturer= google, Name="Nexus 6P", Price=50000 }
            };
        }

        public IActionResult Index(int? companyId)
        {
            // формируем список компаний для передачи в представление
            List<FileModel> fileModels = files
                .Select(c => new FileModel { Id = c.Id, Name = c.Name })
                .ToList();
            // добавляем на первое место
            fileModels.Insert(0, new FileModel { Id = 0, Name = "Все" });

            IndexViewModel ivm = new IndexViewModel { Files = fileModels, Menus = menus };

            // если передан id компании, фильтруем список
            if (companyId != null && companyId > 0)
                ivm.Menus = menus.Where(p => p.Manufacturer.Id == companyId);

            return View(ivm);
        }
    }
}