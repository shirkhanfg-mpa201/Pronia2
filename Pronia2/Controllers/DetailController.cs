using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia2.Contexts;

namespace Pronia2.Controllers
{
    public class DetailController(AppDbContext _context) : Controller
    {
        public IActionResult Index(int id)
        {
            var product = _context.Products.Include(x => x.Category).Include(x => x.ProductImages).Include(x => x.ProductTags).ThenInclude(x => x.Tag).FirstOrDefault(x => x.Id == id);
            return View(product);
          
        }
    }
}
