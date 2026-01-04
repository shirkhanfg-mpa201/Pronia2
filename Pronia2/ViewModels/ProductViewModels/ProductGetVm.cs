namespace Pronia2.ViewModels.ProductViewModels
{
    public class ProductGetVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? SKU { get; set; }
        public int Rating { get; set; }
        public string CategoryName { get; set; }
        public List<string> BrandName { get; set; }
        public string MainImageUrl { get; set; }
        public string HoverImageUrl { get; set; }
        public List<string> TagNames { get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
