namespace SPR411_Shop.Models
{
    public class CartModel
    {
        public int Id { get; set; }
        public int Count { get; set; } = 1;

        public int ProductId { get; set; }
        public ProductModel? Product { get; set; }

        public string? UserId { get; set; }
        public UserModel? User { get; set; }

    }
}
