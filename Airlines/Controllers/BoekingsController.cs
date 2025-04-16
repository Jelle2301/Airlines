using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class BoekingsController : Controller
    {
        public IActionResult Index(string vertrek)
        {
            return View("Index");
        }
    }
}
