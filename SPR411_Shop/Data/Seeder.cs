using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Models;
using System.Text.Json;

namespace SPR411_Shop.Data
{
    public static class Seeder
    {
        public static void Seed(this IApplicationBuilder app)
        {
            // Отримуємо dbContext з DI контейнера
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            dbContext.Database.Migrate();

            if (!dbContext.Categories.Any())
            {
                string filePath = Path.Combine(environment.WebRootPath, "seed_data", "products_categories.json");

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var categories = JsonSerializer.Deserialize<List<CategoryModel>>(json);

                    if (categories != null)
                    {
                        foreach (var category in categories)
                        {
                            foreach (var product in category.Products)
                            {
                                product.Rating = new Random().Next(1, 6);
                            }
                        }

                        dbContext.Categories.AddRange(categories);
                        dbContext.SaveChanges();
                    }

                }
            }
        }
    }
}
