using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;

namespace Pronia2.Areas.Admin.Controllers;
    [Area("Admin")]

    public class CategoryController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id) {
            var category = _context.Categories.Find(id);
            if(category ==null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category category)
        {
            var updatedCategory = _context.Categories.Find(category.Id);
            if (updatedCategory == null) return NotFound();
            if (!ModelState.IsValid)
            {
                return View();
            }
            updatedCategory.Name = category.Name;
            _context.Categories.Update(updatedCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
