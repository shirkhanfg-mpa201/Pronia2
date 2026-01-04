using Microsoft.EntityFrameworkCore;
using Pronia2.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pronia2.Models
{
    public class Product: BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Precision(10, 2)]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? SKU { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public string MainImageUrl { get; set; }
        [Required]
        public string HoverImageUrl { get; set; }

        [Range (1,5)]
        public int Rating { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; } = [];
        public ICollection<ProductImage> ProductImages { get; set; } = [];
        public ICollection<ProductBrand> ProductBrands { get; set; } = [];



    }
}
