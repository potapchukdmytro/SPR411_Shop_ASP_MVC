using Microsoft.AspNetCore.Mvc;

namespace SPR411_Shop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
