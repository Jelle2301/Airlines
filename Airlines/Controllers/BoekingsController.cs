using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class BoekingsController : Controller
    {
        public IActionResult Index(int Id)
        {
            return View();
        }
    }
}
