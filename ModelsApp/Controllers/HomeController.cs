﻿using System.Collections.Generic;
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

        int curr_file = 0;

        private readonly MenuRepository sMenuRepository;
        private readonly FileRepository sfileRepository;
        

        public HomeController(IConfiguration configuration)
        {
            sMenuRepository = new MenuRepository(configuration);
            sfileRepository = new FileRepository(configuration);

            File apple = new File { Id = 1};
            File microsoft = new File { Id = 2};
            File google = new File { Id = 3};

            files = new List<File> { apple, microsoft, google };

            var menus = sMenuRepository.FindAll();            
          
        }

        public IActionResult ViewTable()
        {
            return PartialView("_Table");
        }

        public IActionResult ViewCreate()
        {
            return PartialView("_Create");
        }

        [HttpPost]
        public IActionResult Create(Menu cust)
        {
            if (ModelState.IsValid)
            {
                sMenuRepository.Add(cust);
                return RedirectToAction("Index", new { fileID = cust.File_Id });               
            }
            return View(cust);

        }

        public IActionResult ViewEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Menu obj = sMenuRepository.FindByID(id.Value);

            if (obj == null)
            {
                return NotFound();
            }
            return PartialView("_Edit", obj);
        }

        [HttpPost]
        public IActionResult ViewEdit(Menu obj)
        {
            if (ModelState.IsValid)
            {
                sMenuRepository.Update(obj);
                var file_id = sfileRepository.FindMax();

                curr_file = sMenuRepository.FindCurrentFile(obj);
                return RedirectToAction("Index", new { fileID = curr_file });             
            }

            return View(obj);
        }       


        // GET:/Menu Row/Delete/1
        public IActionResult Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            curr_file = sMenuRepository.FindCurrentFileByID(id.Value);
            sMenuRepository.Remove(id.Value);
            return RedirectToAction("Index", new { fileID = curr_file });          
        }

        public IActionResult Index(int? fileID = 2)
        {
           
            var menus = sMenuRepository.FindAll();

            var tmp = sfileRepository.FindAll();
            var file_id = sfileRepository.FindMax();
            
           
            ViewData["file_id_next"] = file_id.FirstOrDefault() + 1;         

            List<FileModel> fileModels = files
                .Select(c => new FileModel { Id = c.Id })
                .ToList();                     

            IndexViewModel ivm = new IndexViewModel { Files = fileModels, Menus = menus};

            // если передан id компании, фильтруем список
            ViewData["file_id"] = 1;
            ViewData["file_name"] = "PDF/2.pdf";
            if (fileID != null && fileID > 0)
            {
                curr_file = fileID.Value;
                ViewData["file_id"] = fileID.Value;
                ViewData["file_name"] = "PDF/" + fileID.Value.ToString() + ".pdf";
                ivm.Menus = menus.Where(p => p.File_Id == fileID);
            }
            return View(ivm);
        }

        public IActionResult About()
        {
            return View();
        }
               
    }
}