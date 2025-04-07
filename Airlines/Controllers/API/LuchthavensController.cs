using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers.API
{
    public class LuchthavensController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
