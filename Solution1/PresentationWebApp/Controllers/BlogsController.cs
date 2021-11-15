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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace PresentationWebApp.Controllers
{
    [Authorize]
    public class BlogsController : Controller
    {
        private IWebHostEnvironment hostEnvironment;
        private IBlogService service;
        private ICategoryService categoryService;
        private ILogger<BlogsController> logger;
        public BlogsController(ILogger<BlogsController> _logger, IBlogService _service, ICategoryService _categoryService, IWebHostEnvironment _hostEnvironment)
        {
            logger = _logger;
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
        public IActionResult Create()
        {
            var list = categoryService.GetCategories();
            ViewBag.Categories = list;


            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        public IActionResult Create(BlogCreationModel model, IFormFile logoFile)
        {
            try
            {
                logger.Log(LogLevel.Information, "User accessed the Create method");

                if (string.IsNullOrEmpty(model.Name))
                {
                    ViewBag.Error = "Name should not be left empty";
                }
                else
                {
                    if (logoFile != null)
                    {
                        //save the file

                        //1. generate a new unique filename for the file

                        string newFilename = Guid.NewGuid() + System.IO.Path.GetExtension(logoFile.FileName);
                        logger.Log(LogLevel.Information, $"Guid generated for file {logoFile.FileName} is {newFilename}");

                        //2. get the absolute path of the folder "Files"
                        string absolutePath = hostEnvironment.WebRootPath + "\\Files\\" + newFilename;
                        logger.Log(LogLevel.Information, $"Absolute path read is {absolutePath}");
                        //3. save the file into the absolute Path
                        using (FileStream fs = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write))
                        {
                            logoFile.CopyTo(fs);
                            fs.Close();
                        }

                        logger.Log(LogLevel.Information, "File was saved successfully");
                        model.LogoImagePath = "\\Files\\" + newFilename;
                    }

                    service.AddBlog(model);
                    ViewBag.Message = "Blog added successfully";
                }
            }
            catch (Exception ex)
            {
                //log ex
                logger.Log(LogLevel.Error, ex, "Error occurred while uploading file " + logoFile.FileName);
                ViewBag.Error = "Blog was not added due to an error. try later";

            }
            var list = categoryService.GetCategories();
            ViewBag.Categories = list;
            return View();
        }


        public IActionResult Delete (int id)
        {
            try
            {
                var blog = service.GetBlog(id);
                string absolutePathOfImageToDelete = hostEnvironment.WebRootPath + blog.LogoImagePath;

                service.DeleteBlog(id);

                System.IO.File.Delete(absolutePathOfImageToDelete);

                //ViewBag.Message = "Blog deleted successfully";

                TempData["Message"] = "Blog deleted successfully";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            //ViewBag/ViewData does not survive a redirection
            //TempData survives a redirection
            //ControllerContext.HttpContext.Session survives every redirection but it saves the data on the server

            return RedirectToAction("Index");
        }

    }
}
