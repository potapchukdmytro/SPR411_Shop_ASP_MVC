using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Data;
using SPR411_Shop.Models;
using SPR411_Shop.Services;
using SPR411_Shop.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace SPR411_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<UserModel> _userManager;

        public HomeController(AppDbContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private bool IsAuthenticated()
        {
            return User.Identity != null && User.Identity.IsAuthenticated;
        }

        private string? GetUserId()
        {
            if(IsAuthenticated())
            {
                var claim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                return claim != null ? claim.Value : null;
            }

            return null;
        }

        // Home/Index
        public async Task<IActionResult> Index([FromQuery] string? category, [FromQuery] PaginationVM pagination)
        {
            if(IsAuthenticated())
            {
                var userId = GetUserId();
                if(userId != null)
                {
                    var userCartItems = await _context.CartItems
                        .Where(i => i.UserId == userId)
                        .ToListAsync();

                    var cartItemsVm = userCartItems.Select(i => new SessionCartItemVM
                    {
                        ProductId = i.ProductId,
                        Count = i.Count
                    });

                    CartService.SetItems(HttpContext.Session, cartItemsVm);
                }
            }


            var categories = await _context.Categories.ToListAsync();
            IQueryable<ProductModel> products = _context.Products;

            if (!string.IsNullOrWhiteSpace(category))
            {

                var queryCategory = categories
                    .FirstOrDefault(c => c.Name.ToLower() == category.ToLower());

                if (queryCategory == null)
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

        public IActionResult AddToCart(int productId)
        {
            if (!CartService.IsInCart(HttpContext.Session, productId))
            {
                if (IsAuthenticated())
                {
                    var userId = GetUserId();
                    if (userId != null)
                    {
                        _context.CartItems.Add(new CartModel
                        {
                            ProductId = productId,
                            UserId = userId
                        });
                        _context.SaveChanges();
                    }
                }
            }

            CartService.AddToCart(HttpContext.Session, productId);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            if (CartService.IsInCart(HttpContext.Session, productId))
            {
                if (IsAuthenticated())
                {
                    var userId = GetUserId();
                    if (userId != null)
                    {
                        var item = _context.CartItems.FirstOrDefault(i => i.ProductId == productId && i.UserId == userId);
                        if(item != null)
                        {
                            _context.CartItems.Remove(item);
                            _context.SaveChanges();
                        }
                    }
                }
            }

            CartService.RemoveFromCart(HttpContext.Session, productId);
            return RedirectToAction("Index");
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
