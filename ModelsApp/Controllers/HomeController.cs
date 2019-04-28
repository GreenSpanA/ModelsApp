using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ModelsApp.Models; // пространство имен моделей
using ModelsApp.ViewModels;
using ModelsApp.Repository;
using Microsoft.Extensions.Configuration;

namespace ModelsApp.Controllers
{
    public class HomeController : Controller
    {
        List<File> files;
        List<Menu> menus;

        private readonly MenuRepository sMenuRepository;

        public HomeController(IConfiguration configuration)
        {
            sMenuRepository = new MenuRepository(configuration);

            File apple = new File { Id = 1, Name = "Apple" };
            File microsoft = new File { Id = 2, Name = "Microsoft" };
            File google = new File { Id = 3, Name = "Google" };

            files = new List<File> { apple, microsoft, google };

            menus = new List<Menu>
            {
                new Menu { Id=1, File_Name= "1", Dish="iPhone 6S", Price= "56000" },
                new Menu { Id=2, File_Name= "1", Dish="iPhone 5S", Price= "41000" },
                new Menu { Id=3, File_Name= "2", Dish="Lumia 550", Price= "9000" },
                new Menu { Id=4, File_Name= "2", Dish="Lumia 950", Price= "40000" },
                new Menu { Id=5, File_Name= "3", Dish="Nexus 5X", Price= "30000" },
                new Menu { Id=6, File_Name= "3", Dish="Nexus 6P", Price= "50000" }

            };
        }

        public IActionResult Index(int? fileID)
        {
            //var menus = sMenuRepository.FindAll();


            // формируем список компаний для передачи в представление
            List<FileModel> fileModels = files
                .Select(c => new FileModel { Id = c.Id, Name = c.Name })
                .ToList();
            // добавляем на первое место
            fileModels.Insert(0, new FileModel { Id = 0, Name = "Все" });

            IndexViewModel ivm = new IndexViewModel { Files = fileModels, Menus = menus};

            // если передан id компании, фильтруем список
            if (fileID != null && fileID > 0)
                ivm.Menus = menus.Where(p => p.File_Name == fileID.ToString());

            return View(ivm);
        }
    }
}