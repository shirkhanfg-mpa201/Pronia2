using Microsoft.AspNetCore.Mvc;

namespace Pronia2.Areas.Admin.Controllers;
[Area("Admin")]

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
