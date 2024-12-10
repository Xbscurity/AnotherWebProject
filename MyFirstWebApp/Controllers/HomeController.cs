using Microsoft.AspNetCore.Mvc;
using MyFirstApp.Models;

namespace MyFirstApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Apple", Price = 1.50m },
                new Product { Id = 2, Name = "Banana", Price = 1.00m },
                new Product { Id = 3, Name = "Cherry", Price = 2.00m }
            };

            return View(products);  // Передаем список продуктов в представление
        }
    }
}
