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
        




        /*
        public IActionResult Delete(int? vluchtId)
        {
            if (bierNr == null)
            {
                return NotFound();
            }
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");//hoofdlettergevoelig

            CartVM? itemToRemove = cartList?.Carts?.FirstOrDefault(r => r.BeerNumber == bierNr);

            if (itemToRemove != null)
            {
                cartList?.Carts?.Remove(itemToRemove);
                HttpContext.Session.SetObject("ShoppingCart", cartList);
            }
            return View("index", cartList);
        }
        */
    }
}
