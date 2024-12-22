using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstApp.Models;

namespace MyFirstWebApp.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetFilteredProducts(string searchString, decimal? minPrice, decimal? maxPrice, int? categoryId);
        Task<Product?> GetProductByIdAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}
