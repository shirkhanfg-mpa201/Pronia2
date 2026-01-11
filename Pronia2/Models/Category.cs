using Pronia2.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Pronia2.Models
{
    public class Category: BaseEntity
    {
  
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } 
    }
}
