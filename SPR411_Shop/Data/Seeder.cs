using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SPR411_Shop.Models;
using System.Text.Json;

namespace SPR411_Shop.Data
{
    public static class Seeder
    {
        public async static Task Seed(this IApplicationBuilder app)
        {
            // Отримуємо dbContext з DI контейнера
            using var scope = app.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserModel>>();
            using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            dbContext.Database.Migrate();

            // Roles and Users
            if (!roleManager.Roles.Any())
            {
                var adminRole = new IdentityRole { Name = "admin" };
                var userRole = new IdentityRole { Name = "user" };
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);

                var admin = new UserModel
                {
                    Email = "admin@mail.com",
                    EmailConfirmed = true,
                    UserName = "admin",
                    FirstName = "John",
                    LastName = "Smith"
                };

                var user = new UserModel
                {
                    Email = "user@mail.com",
                    EmailConfirmed = true,
                    UserName = "user",
                    FirstName = "Mike",
                    LastName = "Thomson"
                };
                await userManager.CreateAsync(admin, "qwerty");
                await userManager.CreateAsync(user, "qwerty");

                await userManager.AddToRoleAsync(admin, "admin");
                await userManager.AddToRoleAsync(user, "user");
            }

            // Categories and Products
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
