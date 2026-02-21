using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Models;

namespace SPR411_Shop.Data
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            // Отримуємо dbContext з DI контейнера
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();

            if (!dbContext.Categories.Any())
            {
                var categories = new List<CategoryModel>
                {
                    new CategoryModel
                    {
                        Name = "Процесори",
                        Icon = "bi bi-cpu",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "AMD Ryzen 5 5600X", Description = "6 ядер, 3.7–4.6 GHz", Price = 5999, Image = "https://m.media-amazon.com/images/I/61vGQNUEsGL._AC_SL1500_.jpg" },
                            new() { Name = "Intel Core i5-12400F", Description = "6 ядер, до 4.4 GHz", Price = 7200, Image = "https://m.media-amazon.com/images/I/61uJwN8h7PL._AC_SL1500_.jpg" },
                            new() { Name = "AMD Ryzen 7 5800X", Description = "8 ядер, 16 потоків", Price = 10500, Image = "https://m.media-amazon.com/images/I/61W7X9vEwQL._AC_SL1500_.jpg" },
                            new() { Name = "Intel Core i7-12700K", Description = "12 ядер", Price = 14999, Image = "https://m.media-amazon.com/images/I/71eK8k0q9XL._AC_SL1500_.jpg" },
                            new() { Name = "AMD Ryzen 9 5900X", Description = "12 ядер, 24 потоки", Price = 16500, Image = "https://m.media-amazon.com/images/I/61VfL-aiToL._AC_SL1500_.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "Відеокарти",
                        Icon = "bi bi-gpu-card",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "NVIDIA RTX 3060", Description = "12GB GDDR6", Price = 13500, Image = "https://m.media-amazon.com/images/I/71d1i7e4jSL._AC_SL1500_.jpg" },
                            new() { Name = "NVIDIA RTX 4070", Description = "12GB GDDR6X", Price = 29999, Image = "https://m.media-amazon.com/images/I/71lVwl3q-kL._AC_SL1500_.jpg" },
                            new() { Name = "AMD RX 6600", Description = "8GB", Price = 10500, Image = "https://m.media-amazon.com/images/I/81y6Q5KfGZL._AC_SL1500_.jpg" },
                            new() { Name = "AMD RX 6700 XT", Description = "12GB", Price = 14999, Image = "https://m.media-amazon.com/images/I/71gP4x2K6TL._AC_SL1500_.jpg" },
                            new() { Name = "NVIDIA RTX 4090", Description = "24GB GDDR6X", Price = 89999, Image = "https://m.media-amazon.com/images/I/81Z8RZ9h1wL._AC_SL1500_.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "Материнські плати",
                        Icon = "bi bi-motherboard",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "ASUS TUF B550-PLUS", Description = "AM4, ATX", Price = 5200, Image = "https://m.media-amazon.com/images/I/81kY0z0g8kL._AC_SL1500_.jpg" },
                            new() { Name = "MSI B660M PRO", Description = "LGA1700", Price = 4700, Image = "https://m.media-amazon.com/images/I/81J7m5q3cFL._AC_SL1500_.jpg" },
                            new() { Name = "Gigabyte Z790 AORUS", Description = "DDR5", Price = 12500, Image = "https://m.media-amazon.com/images/I/81PqQn0JvFL._AC_SL1500_.jpg" },
                            new() { Name = "ASRock B450M Steel Legend", Description = "mATX", Price = 3900, Image = "https://m.media-amazon.com/images/I/81Md9PLkVYL._AC_SL1500_.jpg" },
                            new() { Name = "MSI X570 Gaming Plus", Description = "ATX", Price = 7600, Image = "https://m.media-amazon.com/images/I/81bsoJ0S1EL._AC_SL1500_.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "Оперативна пам'ять",
                        Icon = "bi bi-memory",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "Kingston Fury 16GB", Description = "DDR4 3200MHz", Price = 1800, Image = "https://m.media-amazon.com/images/I/71z1QF1sZsL._AC_SL1500_.jpg" },
                            new() { Name = "Corsair Vengeance 32GB", Description = "DDR4 3600MHz", Price = 4200, Image = "https://m.media-amazon.com/images/I/71XkE9v3Y9L._AC_SL1500_.jpg" },
                            new() { Name = "G.Skill Trident Z 16GB", Description = "RGB", Price = 2500, Image = "https://m.media-amazon.com/images/I/81hH9k9QZ5L._AC_SL1500_.jpg" },
                            new() { Name = "Kingston Fury Beast 32GB", Description = "DDR5", Price = 6200, Image = "https://m.media-amazon.com/images/I/71jvHc2W5UL._AC_SL1500_.jpg" },
                            new() { Name = "Corsair Dominator 64GB", Description = "DDR5", Price = 14500, Image = "https://m.media-amazon.com/images/I/71QJ7tG6K6L._AC_SL1500_.jpg" }
                        }
                    },

                    new CategoryModel
                    {
                        Name = "SSD накопичувачі",
                        Icon = "bi bi-device-ssd",
                        Products = new List<ProductModel>
                        {
                            new() { Name = "Samsung 970 EVO Plus 1TB", Description = "NVMe", Price = 3600, Image = "https://m.media-amazon.com/images/I/81tw+Vh3J7L._AC_SL1500_.jpg" },
                            new() { Name = "WD Black SN850 1TB", Description = "PCIe 4.0", Price = 4200, Image = "https://m.media-amazon.com/images/I/81m0yZ5H6FL._AC_SL1500_.jpg" },
                            new() { Name = "Kingston NV2 1TB", Description = "Gen4", Price = 2500, Image = "https://m.media-amazon.com/images/I/71A9c6eW1QL._AC_SL1500_.jpg" },
                            new() { Name = "Crucial MX500 1TB", Description = "SATA", Price = 2800, Image = "https://m.media-amazon.com/images/I/71l6cS6n5TL._AC_SL1500_.jpg" },
                            new() { Name = "Samsung 990 PRO 2TB", Description = "NVMe", Price = 8500, Image = "https://m.media-amazon.com/images/I/81Zt42ioCgL._AC_SL1500_.jpg" }
                        }
                    }
                };

                dbContext.Categories.AddRange(categories);
                dbContext.SaveChanges();
            }
        }
    }
}
