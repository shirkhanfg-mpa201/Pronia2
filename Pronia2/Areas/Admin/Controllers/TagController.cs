using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels.TagViewModels;

namespace Pronia2.Areas.Admin.Controllers;
[Area("Admin")]
[AutoValidateAntiforgeryToken]
public class TagController(AppDbContext _context) : Controller
{
    public IActionResult Index()
    {
        var tags = _context.Tags.Select(x => new TagGetVm()
        {
            Id = x.Id,
            Name = x.Name,
        }).ToList();

        return View(tags);
    }



    [HttpGet]
    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(TagCreateVm tag)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Tag newTag = new Tag()
        {
            Name = tag.Name,
        };
        _context.Tags.Add(newTag);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    public IActionResult Delete(int id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null) return NotFound();
        _context.Tags.Remove(tag);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public IActionResult Update(int id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null) return NotFound();
        TagUpdateVm vm = new TagUpdateVm()
        {
            Id = tag.Id,
            Name = tag.Name,
        };
        return View(vm);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(TagUpdateVm tag)
    {
        var updatedTag = _context.Tags.Find(tag.Id);
        if (updatedTag == null) return NotFound();

        if (!ModelState.IsValid)
        {
            return View();
        }
        updatedTag.Name = tag.Name;
        _context.Tags.Update(updatedTag);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}

