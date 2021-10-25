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
        public BlogsController(IBlogService _service)
        {
            service = _service;
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
    }
}
