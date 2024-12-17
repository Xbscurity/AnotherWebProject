using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Data;
using MyFirstWebApp.Services;

namespace MyFirstWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        public async Task<IActionResult> CreateCategory()
        {
            return View();
        }
    } 
}
