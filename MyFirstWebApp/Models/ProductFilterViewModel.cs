using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstApp.Models;

namespace MyFirstWebApp.Models
{
    public class ProductFilterViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int? SelectedCategoryId { get; set; }
        public string SearchString { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
