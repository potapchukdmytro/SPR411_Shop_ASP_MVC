using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Data;
using SPR411_Shop.Models;
using SPR411_Shop.ViewModels;
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
        public async Task<IActionResult> Index([FromQuery]string? category, [FromQuery]PaginationVM pagination)
        {
            var categories = await _context.Categories.ToListAsync();
            IQueryable<ProductModel> products = _context.Products;

            if (!string.IsNullOrWhiteSpace(category))
            {

                var queryCategory = categories
                    .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());

                if(queryCategory == null)
                {
                    return RedirectToAction("Index");
                }

                products = products.Where(p => p.CategoryId == queryCategory.Id);
            }

            // pagination
            int totalCount = products.Count();
            int pageSize = pagination.PageSize < 1 ? 24 : pagination.PageSize;
            int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);
            int page = pagination.Page < 1 || pagination.Page > pageCount ? 1 : pagination.Page;

            pagination.Page = page;
            pagination.TotalCount = totalCount;
            pagination.PageCount = pageCount;
            pagination.PageSize = pageSize;

            products = products
                .OrderBy(p => p.Id)
                .Skip(pageSize * (page - 1))
                .Take(pageSize);

            var viewModel = new HomeVM
            {
                Products = await products.ToListAsync(),
                Categories = categories,
                Pagination = pagination,
                Category = category
            };

            return View(viewModel);
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
