using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstApp.Models;
using MyFirstWebApp.Models;
using MyFirstWebApp.Services;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(string searchString, decimal? minPrice, decimal? maxPrice)
    {
        var products = await _productService.GetFilteredProducts(searchString, minPrice, maxPrice);
        return View(products);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var product = await _productService.GetProductByIdAsync(id.Value);
        if (product == null) return NotFound();

        return View(product);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        // Получаем список категорий через сервис
        var viewModel = new ProductViewModel
        {
            Categories = await _productService.GetCategoriesAsync() // Получаем категории
        };
        return View(viewModel);
    }




    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(ProductViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId
            };

            await _productService.CreateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        viewModel.Categories = await _productService.GetCategoriesAsync();
        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var product = await _productService.GetProductByIdAsync(id.Value);
        if (product == null) return NotFound();

        var viewModel = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Categories = await _productService.GetCategoriesAsync()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, ProductViewModel viewModel)
    {
        if (id != viewModel.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Price = viewModel.Price,
                CategoryId = viewModel.CategoryId
            };

            await _productService.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        viewModel.Categories = await _productService.GetCategoriesAsync();
        return View(viewModel);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var product = await _productService.GetProductByIdAsync(id.Value);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
