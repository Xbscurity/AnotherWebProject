using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyFirstWebApp.Data;
using MyFirstWebApp.Models;
using System;
using System.Threading.Tasks;

namespace MyFirstWebApp.Seeds
{
    public static class ApplicationDbInitializer
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();


            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }


            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true 
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception($"Не удалось создать пользователя: {string.Join(", ", result.Errors)}");
                }
            }
        }
        public static void InitializeProductCategory(ApplicationDbContext context)
        {

            if (context.ProductCategories.Any())
            {
                return;   
            }

            var categories = new Category[]
            {
            new Category { Name = "Electronics" },
            new Category { Name = "Clothing" },
            new Category { Name = "Food" },
            new Category { Name = "Books" }
            };

            foreach (var category in categories)
            {
                context.ProductCategories.Add(category);
            }

            context.SaveChanges(); 
        }
    }
}
