using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationWebApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult HandleQuery(string query)
        {
            //process the query e.g. save it in db, or send out an email

            if (string.IsNullOrEmpty(query) == false)
                ViewBag.Message = "Your query was received!";
            else ViewBag.Message = "No query was input";


            return View("Index"); //sends back the Index.cshtml page to the user
                                  //return View() will send back a page having the same name as the method

        }
    }
}
