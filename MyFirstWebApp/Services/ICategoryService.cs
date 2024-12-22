using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;

namespace MyFirstWebApp.Services
{
    public interface ICategoryService
    {
         Task CreateCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task<Category?> GetCategoryByIdAsync(int? id);
        Task<bool> IsCategoryExistsAsync(string name);

         Task<List<Category>> GetAllCategoriesAsync();

         Task DeleteCategoryAsync(int id);
    }
}

