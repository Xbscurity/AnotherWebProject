using MyFirstApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
    public class Category
    {
        public int Id { get; set; } // Первичный ключ
        public string Name { get; set; } = string.Empty;

        // Навигационное свойство для связи с продуктами
        public ICollection<Product>? Products { get; set; }
    }

}
