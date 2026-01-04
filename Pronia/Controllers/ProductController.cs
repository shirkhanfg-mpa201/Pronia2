using Microsoft.AspNetCore.Mvc;
using Pronia.Contexts;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Controllers
{
    public class ProductController (AppDbContext _context) : Controller
    {
        public IActionResult Index()
        {
            var products = _context.Products.Include(x=>x.Category).ToList();
            return View(products);
        }
    }
}
