using Pronia2.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pronia2.Models
{
    public class ProductImage: BaseEntity
    {
        [Required]
        [MinLength(3), MaxLength(512)]
        public string ImageUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
