using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels;

namespace Pronia2.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
           HomeViewModel homeViewModel = new HomeViewModel
           {
                Sliders = _context.Sliders.ToList(),
               Services = _context.Services.ToList()
           };
    
                return View(homeViewModel);

        }

    }
}
