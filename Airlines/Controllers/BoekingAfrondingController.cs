using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class BoekingAfrondingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShoppingCartLeegIndex()
        {
            return View();
        }
    }
}
