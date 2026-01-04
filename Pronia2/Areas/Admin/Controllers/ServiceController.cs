using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;

namespace Pronia2.Areas.Admin.Controllers;
[Area("Admin")]

public class ServiceController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var services = _context.Services.ToList();
        return View(services);
    }

    [HttpGet]

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Service service)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        _context.Services.Add(service);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var service = _context.Services.Find(id);
        if (service == null) return NotFound();
        _context.Services.Remove(service);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public IActionResult Update(int id)
    {
        var service = _context.Services.Find(id);
        if (service == null) return NotFound();
        return View(service);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(Service service)
    {
        var updatedService = _context.Services.Find(service);
        if (service == null) return NotFound();
        if (!ModelState.IsValid)
        {
            return View();

        }
        updatedService.Title = service.Title;
        updatedService.Description = service.Description;
        updatedService.ImageUrl = service.ImageUrl;
        _context.Services.Update(service);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));

    }
}

