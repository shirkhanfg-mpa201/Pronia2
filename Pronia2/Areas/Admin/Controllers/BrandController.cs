using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels.BrandViewModel;

namespace Pronia2.Areas.Admin.Controllers;
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]

public class BrandController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            var brands = _context.Brands.Select(x => new BrandGetVm()
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            return View(brands);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BrandCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            Brand newBrand = new Brand()
            {
                Name = vm.Name
            };

            _context.Brands.Add(newBrand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand == null)
            {
                return NotFound();
            }

            BrandUpdateVm vm = new BrandUpdateVm()
            {
                Id = brand.Id,
                Name = brand.Name,
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(BrandUpdateVm vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var existBrand = _context.Brands.Find(vm.Id);
            if (existBrand == null)
            {
                return NotFound();
            }
            existBrand.Name = vm.Name;
            _context.Brands.Update(existBrand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

