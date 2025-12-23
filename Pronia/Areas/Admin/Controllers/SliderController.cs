using Microsoft.AspNetCore.Mvc;
using Pronia.Contexts;

namespace Pronia.Areas.Admin.Controllers;
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
        if (slider is null)
        {
            return NotFound();
        }

        _context.Sliders.Remove(slider);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}
