using System.ComponentModel.DataAnnotations;

namespace Pronia2.Models
{
    public class Service
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string? Description { get; set; }
        [MaxLength(512),MinLength(3)]
        [Required]
        public string ImageUrl { get; set; }    
    }
}
