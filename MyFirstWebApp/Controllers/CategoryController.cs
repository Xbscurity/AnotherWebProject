using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebApp.Models;
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


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            var categoryvm = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
            return View(categoryvm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Category categoryvm)
        {
            if (id == null || id != categoryvm.Id) return BadRequest();
            if (!ModelState.IsValid) return View(categoryvm);

            var category = new Category
            {
                Id = categoryvm.Id,
                Name = categoryvm.Name.Trim()
            };
            await _categoryService.UpdateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryvm)
        {
            if (await _categoryService.IsCategoryExistsAsync(categoryvm.Name))
            {
                ModelState.AddModelError("Name", "Category with this name already exists.");
                return View(categoryvm);
            }

            if (!ModelState.IsValid) return View(categoryvm);

            var category = new Category
            {
                Name = categoryvm.Name.Trim()
            };

            await _categoryService.CreateCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
