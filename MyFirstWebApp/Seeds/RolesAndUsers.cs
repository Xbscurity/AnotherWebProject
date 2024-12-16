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
            // Получаем RoleManager и UserManager через сервисы
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Проверяем и создаем роли, если их нет
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            // Создаем администратора, если его ещё нет
            var adminEmail = "admin@example.com";
            var adminPassword = "Admin@123";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Подтверждение почты сразу
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    // Добавляем пользователя в роль Admin
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
            // Если уже есть данные, не делаем вставку
            if (context.ProductCategories.Any())
            {
                return;   // База данных уже содержит категории
            }

            // Создание нескольких категорий
            var categories = new ProductCategory[]
            {
            new ProductCategory { Name = "Electronics" },
            new ProductCategory { Name = "Clothing" },
            new ProductCategory { Name = "Food" },
            new ProductCategory { Name = "Books" }
            };

            foreach (var category in categories)
            {
                context.ProductCategories.Add(category);
            }

            context.SaveChanges(); // Сохраняем изменения в базу данных
        }
    }
}
