using Airlines.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index(VluchtVM vluchtVM)
        {

            return View();
        }
    }
}
