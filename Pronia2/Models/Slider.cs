using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Pronia2.Models
{
    public class Slider
    {
       // [Key]
        public int Id { get; set; }
     
        public string Title { get; set; }

        public string? Description { get; set; }

        public decimal DiscountPercentage { get; set; }

        public string ImageUrl { get; set; }
    }
}
