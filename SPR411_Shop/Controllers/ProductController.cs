using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Data;
using SPR411_Shop.Models;
using SPR411_Shop.ViewModels;

namespace SPR411_Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> products = _context.Products
                .Include(p => p.Category)
                .AsEnumerable();

            return View(products);
        }

        // get
        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.ToListAsync();

            var viewModel = new CreateProductVM
            {
                SelectItems = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };


            return View(viewModel);
        }

        // post
        // [FromForm] - multipart/form-data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreateProductVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _context.Categories.ToListAsync();
                viewModel.SelectItems = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(viewModel);
            }

            var model = new ProductModel
            {
                CategoryId = viewModel.CategoryId <= 0 ? null : viewModel.CategoryId,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                CreateDate = viewModel.CreateDate
            };

            if (viewModel.Image != null)
            {
                model.Image = await SaveImageAsync(viewModel.Image);
            }

            await _context.Products.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product != null)
            {
                if(!string.IsNullOrWhiteSpace(product.Image))
                {
                    // Remove image file
                    string root = _environment.WebRootPath;
                    string imagesPath = Path.Combine(root, "images", "products");
                    string filePath = Path.Combine(imagesPath, product.Image);

                    if(System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        private async Task<string?> SaveImageAsync(IFormFile file)
        {
            try
            {
                var types = file.ContentType.Split("/");
                if (types.Length != 2 || types[0] != "image")
                {
                    return null;
                }

                string root = _environment.WebRootPath;
                string imagesPath = Path.Combine(root, "images", "products");
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(imagesPath, fileName);

                using var fileStream = System.IO.File.Create(filePath);
                using var imageStream = file.OpenReadStream();
                await imageStream.CopyToAsync(fileStream);

                return fileName;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
