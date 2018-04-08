using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace SqueletteImplantation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }

    }
}
