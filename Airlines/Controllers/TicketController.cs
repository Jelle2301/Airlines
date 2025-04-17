using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
