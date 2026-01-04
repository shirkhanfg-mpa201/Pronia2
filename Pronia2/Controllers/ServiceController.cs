using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels;

namespace Pronia2.Controllers
{
    public class ServiceController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel
            {
                Sliders = _context.Sliders.ToList(),
                Services = _context.Services.ToList()
            };
            return View(model);
        }
    }
}
