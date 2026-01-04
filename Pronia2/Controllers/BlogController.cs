using Microsoft.AspNetCore.Mvc;

namespace Pronia2.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
