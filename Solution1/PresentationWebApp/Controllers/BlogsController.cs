using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services;
using Application.Interfaces;

namespace PresentationWebApp.Controllers
{
    public class BlogsController : Controller
    {
        private IBlogService service;
        private ICategoryService categoryService;
        public BlogsController(IBlogService _service, ICategoryService _categoryService)
        {
            service = _service;
            categoryService = _categoryService;
        }

        public IActionResult Index()
        { 
            var list = service.GetBlogs();
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var b = service.GetBlog(id);
            return View(b);
        }

        public IActionResult Create ()
        {
            var list = categoryService.GetCategories();
            ViewBag.Categories = list;


            return View();
        }
    }
}
