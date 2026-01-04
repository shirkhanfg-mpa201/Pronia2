using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Pronia2.Models
{
    public class Slider
    {
       // [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string? Description { get; set; }
        [Range(0,100)]
        [Precision(10,2)]
        public decimal DiscountPercentage { get; set; }
        [Required]
        [MinLength(3), MaxLength(512)]
        public string ImageUrl { get; set; }
    }
}
