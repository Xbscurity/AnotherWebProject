using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyFirstWebApp.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
