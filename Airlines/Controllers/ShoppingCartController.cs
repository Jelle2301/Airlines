using Airlines.Extensions;
using Airlines.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Airlines.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            
            ShoppingCartVM? cartList =
                HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            return View(cartList);
        }
    }
}
