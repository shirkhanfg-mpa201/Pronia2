using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia2.Contexts;
using Pronia2.Helpers;
using Pronia2.ViewModels.ProductViewModels;

namespace Pronia2.Areas.Admin.Controllers;
[Area("Admin")]

public class ProductController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
{
    public IActionResult Index()
    {
        var vms = _context.Products.Include(x => x.Category).Select(x => new ProductGetVm()
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Description = x.Description,
            Rating = x.Rating,
            SKU = x.SKU,
            CategoryName = x.Category.Name,
            MainImageUrl = x.MainImageUrl,
            HoverImageUrl = x.HoverImageUrl,
            TagNames = x.ProductTags.Select(x => x.Tag.Name).ToList(),

        }).ToList();
        return View(vms);
    }

    [HttpGet]
    public IActionResult Create()
    {
        SendItemsWithViewBag();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductCreateVm vm)
    {
        if (!ModelState.IsValid)
        {
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.MainImage.CheckType("image"))
        {
            ModelState.AddModelError("MainImage", "The format must be image!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.MainImage.CheckSize(2))
        {
            ModelState.AddModelError("MainImage", "Image size cannot be greater than 2 MB!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.HoverImage.CheckType("image"))
        {
            ModelState.AddModelError("HoverImage", "Image format is required!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.HoverImage.CheckSize(2))
        {
            ModelState.AddModelError("HoverImage", "Image size cannot be greater than 2 MB!");
            SendItemsWithViewBag();
            return View(vm);
        }

        foreach (var image in vm.Images)
        {
            if (!image.CheckType("image"))
            {
                ModelState.AddModelError("Images", "Image format is required!");
                SendItemsWithViewBag();
                return View(vm);
            }

            if (!image.CheckSize(2))
            {
                ModelState.AddModelError("Images", "Image size cannot be greater than 2 MB!");
                SendItemsWithViewBag();
                return View(vm);
            }
        }

        var isExistCategory = _context.Categories.Any(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            SendItemsWithViewBag();
            ModelState.AddModelError("CategoryId", "Category is not found!");
            return View(vm);
        }

        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = _context.Tags.Any(x => x.Id == tagId);

            if (!isExistTag)
            {
                SendItemsWithViewBag();
                ModelState.AddModelError("TagIds", "Tag is not found!");
                return View(vm);
            }
        }

        foreach (var brandId in vm.BrandIds)
        {
            var isExistBrand = _context.Brands.Any(x => x.Id == brandId);

            if (!isExistBrand)
            {
                SendItemsWithViewBag();
                ModelState.AddModelError("BrandIds", "Brand is not found!");
                return View(vm);
            }
        }

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images", "website-images");

        string mainImageUniqueName = vm.MainImage.SaveFile(folderPath);
        string hoverImageUniqueName = vm.HoverImage.SaveFile(folderPath);

        Product product = new Product()
        {
            Name = vm.Name,
            CategoryId = vm.CategoryId,
            Description = vm.Description,
            Price = vm.Price,
            SKU = vm.SKU,
            Rating = vm.Rating,
            MainImageUrl = mainImageUniqueName,
            HoverImageUrl = hoverImageUniqueName,
            ProductTags = [],
            ProductBrands = [],
            ProductImages = []
        };

        foreach (var image in vm.Images)
        {
            string imageUniqueName = image.SaveFile(folderPath);
            ProductImage productImage = new ProductImage()
            {
                ImageUrl = imageUniqueName,
                Product = product
            };
            product.ProductImages.Add(productImage);
        }

        foreach (var tagId in vm.TagIds)
        {
            ProductTag productTag = new ProductTag()
            {
                TagId = tagId,
                Product = product
            };
            product.ProductTags.Add(productTag);
        }

        foreach (var brandId in vm.BrandIds)
        {
            ProductBrand productBrand = new ProductBrand()
            {
                BrandId = brandId,
                Product = product
            };
            product.ProductBrands.Add(productBrand);
        }

        _context.Products.Add(product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        SendItemsWithViewBag();

        var product = _context.Products.Include(x => x.ProductTags).Include(x => x.ProductBrands).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        ProductUpdateVm vm = new ProductUpdateVm()
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price,
            Rating = product.Rating,
            SKU = product.SKU,
            TagIds = product.ProductTags.Select(x => x.TagId).ToList(),
            BrandIds = product.ProductBrands.Select(x => x.BrandId).ToList(),
            MainImagePath = product.MainImageUrl,
            HoverImagePath = product.HoverImageUrl,
            ImageUrls = product.ProductImages.Select(x => x.ImageUrl).ToList(),
            ImageIds = product.ProductImages.Select(x => x.Id).ToList(),
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(ProductUpdateVm vm)
    {
        if (!ModelState.IsValid)
        {
            SendItemsWithViewBag();
            return View(vm);
        }

        var existProduct = _context.Products.Include(x => x.ProductTags).Include(x => x.ProductBrands).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == vm.Id);

        if (existProduct is null)
        {
            return NotFound();
        }

        var isExistCategory = _context.Categories.Any(x => x.Id == vm.CategoryId);

        if (!isExistCategory)
        {
            SendItemsWithViewBag();
            ModelState.AddModelError("CategoryId", "Category is not found!");
            return View(vm);
        }

        foreach (var tagId in vm.TagIds)
        {
            var isExistTag = _context.Tags.Any(x => x.Id == tagId);

            if (!isExistTag)
            {
                SendItemsWithViewBag();
                ModelState.AddModelError("TagIds", "Tag is not found!");
                return View(vm);
            }
        }

        foreach (var brandId in vm.BrandIds)
        {
            var isExistBrand = _context.Brands.Any(x => x.Id == brandId);

            if (!isExistBrand)
            {
                SendItemsWithViewBag();
                ModelState.AddModelError("BrandIds", "Brand is not found!");
                return View(vm);
            }
        }


        if (!vm.MainImage?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("MainImage", "The format must be image!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.MainImage?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("MainImage", "Image size cannot be greater than 2 MB!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.HoverImage?.CheckType("image") ?? false)
        {
            ModelState.AddModelError("HoverImage", "Image format is required!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (!vm.HoverImage?.CheckSize(2) ?? false)
        {
            ModelState.AddModelError("HoverImage", "Image size cannot be greater than 2 MB!");
            SendItemsWithViewBag();
            return View(vm);
        }

        if (vm.Images != null)
        {
            foreach (var image in vm.Images)
            {
                if (!image.CheckType("image"))
                {
                    ModelState.AddModelError("Images", "Image format is required!");
                    SendItemsWithViewBag();
                    return View(vm);
                }

                if (!image.CheckSize(2))
                {
                    ModelState.AddModelError("Images", "Image size cannot be greater than 2 MB!");
                    SendItemsWithViewBag();
                    return View(vm);
                }
            }
        }

        existProduct.Name = vm.Name;
        existProduct.Description = vm.Description;
        existProduct.Price = vm.Price;
        existProduct.SKU = vm.SKU;
        existProduct.Rating = vm.Rating;
        existProduct.CategoryId = vm.CategoryId;

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images", "website-images");

        if (vm.MainImage is { })
        {
            string newMainImageName = vm.MainImage.SaveFile(folderPath);

            if (System.IO.File.Exists(Path.Combine(folderPath, existProduct.MainImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(folderPath, existProduct.MainImageUrl));
            }

            existProduct.MainImageUrl = newMainImageName;
        }

        if (vm.HoverImage is { })
        {
            string newHoverImageName = vm.HoverImage.SaveFile(folderPath);

            if (System.IO.File.Exists(Path.Combine(folderPath, existProduct.HoverImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(folderPath, existProduct.HoverImageUrl));
            }

            existProduct.HoverImageUrl = newHoverImageName;
        }

        existProduct.ProductTags = [];
        existProduct.ProductBrands = [];

        foreach (var tagId in vm.TagIds)
        {
            ProductTag productTag = new ProductTag()
            {
                TagId = tagId,
                ProductId = existProduct.Id
            };
            existProduct.ProductTags.Add(productTag);
        }

        foreach (var brandId in vm.BrandIds)
        {
            ProductBrand productBrand = new ProductBrand()
            {
                BrandId = brandId,
                ProductId = existProduct.Id
            };
            existProduct.ProductBrands.Add(productBrand);
        }

        foreach (var productImage in existProduct.ProductImages.ToList())
        {
            var isExistImage = vm.ImageIds.Any(x => x == productImage.Id);

            if (isExistImage == false)
            {
                existProduct.ProductImages.Remove(productImage);

                if (System.IO.File.Exists(Path.Combine(folderPath, productImage.ImageUrl)))
                {
                    System.IO.File.Delete(Path.Combine(folderPath, productImage.ImageUrl));
                }
            }
        }

        if (vm.Images != null)
        {
            foreach (var image in vm.Images)
            {
                string imageUniqueName = image.SaveFile(folderPath);
                ProductImage productImage = new ProductImage()
                {
                    ImageUrl = imageUniqueName,
                    ProductId = existProduct.Id
                };
                existProduct.ProductImages.Add(productImage);
            }
        }

        _context.Products.Update(existProduct);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        _context.SaveChanges();

        string folderPath = Path.Combine(_environment.WebRootPath, "assets", "images", "website-images");

        if (System.IO.File.Exists(Path.Combine(folderPath, product.MainImageUrl)))
        {
            System.IO.File.Delete(Path.Combine(folderPath, product.MainImageUrl));
        }

        if (System.IO.File.Exists(Path.Combine(folderPath, product.HoverImageUrl)))
        {
            System.IO.File.Delete(Path.Combine(folderPath, product.HoverImageUrl));
        }

        foreach (var productImage in product.ProductImages)
        {
            if (System.IO.File.Exists(Path.Combine(folderPath, productImage.ImageUrl)))
            {
                System.IO.File.Delete(Path.Combine(folderPath, productImage.ImageUrl));
            }
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
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
            BrandName = x.ProductBrands.Select(x => x.Brand.Name).ToList(),
            ImageUrls = x.ProductImages.Select(x => x.ImageUrl).ToList(),
        }).FirstOrDefault(x => x.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }

    private void SendItemsWithViewBag()
    {
        var categories = _context.Categories.ToList();
        ViewBag.Categories = categories;

        var tags = _context.Tags.ToList();
        ViewBag.Tags = tags;

        var brands = _context.Brands.ToList();
        ViewBag.Brands = brands;
    }
}