using Microsoft.AspNetCore.Mvc;
using SPR411_Shop.Data;
using SPR411_Shop.Models;
using System.Diagnostics;

namespace SPR411_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // Home/Index
        public IActionResult Index()
        {
            return View();
        }

        //Home/Privacy
        public IActionResult Privacy()
        {
            return View();
        }

        // Home/AboutUs
        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
