using Pronia2.Models.Common;

namespace Pronia2.Models
{
    public class BasketItem:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
        public AppUser AppUser { get; set; } = null!;

        public string AppUserId { get; set; } = null!;

        public int Count { get; set; }
    }
}
