using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;

namespace Pronia2.Areas.Admin.Controllers;
[Area("Admin")]

public class SliderController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var sliders = _context.Sliders.ToList();
        return View(sliders);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Slider slider)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        _context.Sliders.Add(slider);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var slider = _context.Sliders.Find(id);
        if (slider == null) return NotFound();
        _context.Sliders.Remove(slider);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public IActionResult Update(int id)
    {
        var slider= _context.Sliders.Find(id);
        if (slider == null) return NotFound();
        return View(slider);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Slider slider)
    {

        if (!ModelState.IsValid)
        {
            return View();
        }


        var updatedSlider = _context.Sliders.Find(slider.Id);
        if (slider == null) return NotFound();

        updatedSlider.Title = slider.Title;
        updatedSlider.Description = slider.Description;
        updatedSlider.DiscountPercentage = slider.DiscountPercentage;
        updatedSlider.ImageUrl = slider.ImageUrl;
        _context.Sliders.Update(updatedSlider);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));


    }
}
