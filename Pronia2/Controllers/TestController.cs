using Microsoft.AspNetCore.Mvc;
using Pronia2.Abstractions;
using System.Threading.Tasks;

namespace Pronia2.Controllers
{
    public class TestController(IEMailService _service) : Controller
    {
        public async Task<IActionResult> SendEmail()
        {
           await _service.SendEmailAsync("shirkhanfg-mpa201@code.edu.az", "Email Service", "Service is done");
            return Ok("Email sent successfully");
        }
    }
}
