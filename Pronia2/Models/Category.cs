using Pronia2.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pronia2.Models
{
    public class Category: BaseEntity
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}
