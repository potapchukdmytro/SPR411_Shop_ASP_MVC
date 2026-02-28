using SPR411_Shop.Models;

namespace SPR411_Shop.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<ProductModel> Products { get; set; } = [];
        public IEnumerable<CategoryModel> Categories { get; set; } = [];
        public PaginationVM Pagination { get; set; } = new();
        public string? Category { get; set; } = null;
    }
}
