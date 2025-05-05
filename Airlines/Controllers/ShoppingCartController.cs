using Airlines.Extensions;
using Airlines.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace Airlines.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IZitplaatsService _zitplaatsService;
        public ShoppingCartController(IMapper mapper, IZitplaatsService zitplaatsService)
        {
            _mapper = mapper;
            _zitplaatsService = zitplaatsService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ShoppingCartVM? cartList =
                    HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
                //foreach (VluchtVM vlucht in lstVluchtVM)

                foreach (CartVM cartItem in cartList.Carts)
                {
                    var seat = await _zitplaatsService.GetAllBeschikbareZitplaatsenByVluchtAsync(cartItem.Ticket.Vlucht.VluchtId);
                    if (seat != null)
                    {
                        cartItem.Ticket.Zitplaats = _mapper.Map<ZitplaatsVM>(seat);
                        await _zitplaatsService.MaakZitplaatsBezet(cartItem.Ticket.Zitplaats.ZitplaatsId);
                    }
                }
                HttpContext.Session.SetObject("ShoppingCart", cartList);
                return View(cartList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
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
