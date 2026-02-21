namespace SPR411_Shop.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Icon { get; set; }

        public List<ProductModel> Products { get; set; } = [];
    }
}
