using Pronia2.Models.Common;

namespace Pronia2.Models
{
    public class Brand: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductBrand> ProductBrands { get; set; } = [];
    }
}
