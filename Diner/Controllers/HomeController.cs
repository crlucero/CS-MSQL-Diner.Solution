using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Diner.Models;

namespace Diner.Controllers
{
    public class HomeController : Controller
    {
    
    [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
