using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace PresentationWebApp.Controllers
{
    public class BlogsController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        private IBlogService service;
        private ICategoryService categoryService;
        public BlogsController(IBlogService _service, ICategoryService _categoryService, IWebHostEnvironment _hostEnvironment)
        {
            service = _service;
            categoryService = _categoryService;
            hostEnvironment = _hostEnvironment;
        }

        public IActionResult Index()
        { 
            var list = service.GetBlogs();
            return View(list);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var b = service.GetBlog(id);
            return View(b);
        }

        [HttpGet]
        public IActionResult Create ()
        {
            var list = categoryService.GetCategories();
            ViewBag.Categories = list;


            return View();
        }

        [HttpPost]
        public IActionResult Create(BlogCreationModel model, IFormFile logoFile)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Name))
                {
                    ViewBag.Error = "Name should not be left empty";
                }
                else
                {
                    if(logoFile != null)
                    {
                        //save the file

                        //1. generate a new unique filename for the file

                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(logoFile.FileName);

                        //2. get the absolute path of the folder "Files"
                        string absolutePath =  Path.Combine(hostEnvironment.WebRootPath, newFilename);

                        //3. save the file into the absolute Path

                        // FileStream fs = new FileStream("\Files\"+newFilename, FileMode.CreateNew, FileAccess.ReadWrite);
                        //  fs.Close();
                    }

                    service.AddBlog(model);
                    ViewBag.Message = "Blog added successfully";
                }
            }
            catch (Exception ex)
            {
                //log ex

                ViewBag.Error = "Blog was not added due to an error. try later";

            }
            var list = categoryService.GetCategories();
            ViewBag.Categories = list;
            return View();
        }





    }
}
