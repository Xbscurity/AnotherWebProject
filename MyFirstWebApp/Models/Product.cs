using Microsoft.EntityFrameworkCore;
using MyFirstWebApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
