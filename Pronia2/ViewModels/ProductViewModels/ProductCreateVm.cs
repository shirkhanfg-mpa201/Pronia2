using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Pronia2.ViewModels.ProductViewModels
{
    public class ProductCreateVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Precision(10, 2)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? SKU { get; set; }
        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public List<int> BrandIds { get; set; }
        [Required]
        public IFormFile MainImage { get; set; }
        [Required]
        public IFormFile HoverImage { get; set; }
        public List<int> TagIds { get; set; }
        public List<IFormFile> Images { get; set; } = [];
    }
}
