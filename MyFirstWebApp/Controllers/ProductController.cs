using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyFirstApp.Models;
using MyFirstWebApp.Models;
using MyFirstWebApp.Services;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(ProductFilterViewModel filter, string sortOrder)
    {
        // Получение продуктов на основе фильтра
        var products = await _productService.GetFilteredProducts(
            filter.SearchString,
            filter.MinPrice,
            filter.MaxPrice,
            filter.SelectedCategoryId
        );
        if (filter.StartDate.HasValue)
        {
            products = products.Where(p => p.CreatedAt >= filter.StartDate.Value);
        }
        if (filter.EndDate.HasValue)
        {
            products = products.Where(p => p.CreatedAt <= filter.EndDate.Value);
        }
        // Применение сортировки
        products = sortOrder switch
        {
            "name_desc" => products.OrderByDescending(p => p.Name),
            "price" => products.OrderBy(p => p.Price),
            "price_desc" => products.OrderByDescending(p => p.Price),
            "createdat" => products.OrderBy(p => p.CreatedAt),
            "createdat_desc" => products.OrderByDescending(p => p.CreatedAt),
            _ => products.OrderBy(p => p.Name)
        };

        // Получение списка категорий для выпадающего списка
        filter.Categories = await _productService.GetCategoriesAsync();

        // Присваиваем отфильтрованные продукты
        filter.Products = products;

        return View(filter);
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
        var viewModel = new ProductViewModel
        {
            Categories = await _productService.GetCategoriesAsync() 
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
        if (!id.HasValue)
            return NotFound("Идентификатор продукта отсутствует.");

        var product = await _productService.GetProductByIdAsync(id.Value);
        if (product == null)
            return NotFound();
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
