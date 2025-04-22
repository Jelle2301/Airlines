using Airlines.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class TicketController : Controller
    {
        public IActionResult Index(VluchtVM vluchtVM)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }

            return View();
        }
    }
}
