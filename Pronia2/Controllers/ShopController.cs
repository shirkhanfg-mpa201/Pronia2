using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels.ProductViewModels;


namespace Pronia.Controllers
{
    public class ShopController(AppDbContext _context) : Controller
       
    {
        [Authorize]
        public IActionResult Index()
        {
            var products = _context.Products.Select(product => new ProductGetVm()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                SKU = product.SKU,
                Rating = product.Rating,
                Description = product.Description,
                CategoryName = product.Category.Name,
                MainImageUrl = product.MainImageUrl,
                HoverImageUrl = product.HoverImageUrl
            }).ToList();

            TempData["ErrorMessage"] = "You must be logged in to access the shop.";
            return View(products);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products.Select(x => new ProductGetVm()
            {
                Id = x.Id,
                CategoryName = x.Category.Name,
                Description = x.Description,
                Name = x.Name,
                HoverImageUrl = x.HoverImageUrl,
                MainImageUrl = x.MainImageUrl,
                Price = x.Price,
                SKU = x.SKU,
                Rating = x.Rating,
                TagNames = x.ProductTags.Select(x => x.Tag.Name).ToList(),
                BrandName = x.ProductBrands.Select(x => x.Brand.Name).ToList()
            }).FirstOrDefault(x => x.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
