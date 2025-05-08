using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Airlines.Extensions;
using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using SQLitePCL;

namespace Airlines.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IZitplaatsService _zitplaatsService;
        private readonly ITicketService _ticketService;
        private readonly IBoekingService _boekingService;

        public ShoppingCartController(IMapper mapper, IZitplaatsService zitplaatsService, ITicketService ticketService, IBoekingService boekingService)
        {
            _mapper = mapper;
            _zitplaatsService = zitplaatsService;
            _ticketService = ticketService;
            _boekingService = boekingService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ShoppingCartVM? cartList =
                    HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");


                foreach (CartVM cartItem in cartList.Carts)
                {
                    var seat = await _zitplaatsService.GetAllBeschikbareZitplaatsenByVluchtAsync(cartItem.Ticket.Vlucht.VluchtId);
                    if (seat != null)
                    {
                        cartItem.Ticket.Zitplaats = _mapper.Map<ZitplaatsVM>(seat);
                        cartItem.Ticket.Zitplaats.ZitplaatsId = cartItem.Ticket.Zitplaats.ZitplaatsId;
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
        [HttpPost]
        public async Task<IActionResult> PostData()
        {
            try
            {
                ShoppingCartVM? cartList =
                    HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
                if (cartList == null)
                {
                    return RedirectToAction("ShoppingCartLeegIndex", "BoekingAfronding");
                }
                if (cartList.Carts == null)
                {
                    return RedirectToAction("ShoppingCartLeegIndex", "BoekingAfronding");
                }
                foreach (CartVM cartItem in cartList.Carts)
                {
                    var ticketEntity = _mapper.Map<Ticket>(cartItem.Ticket);
                    await _ticketService.AddAsync(ticketEntity);

                    var boekingVM = new BoekingVM();
                    boekingVM.TicketId = ticketEntity.TicketId;
                    boekingVM.DatumBoeking = DateOnly.FromDateTime(DateTime.Now);
                    boekingVM.Status = "Voltooid";
                    boekingVM.TotalePrijs = ticketEntity.Prijs;
                    boekingVM.VoornaamBoeking = ticketEntity.Voornaam;
                    boekingVM.AchternaamBoeking = ticketEntity.Achternaam;
                    boekingVM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var boekingEntity = _mapper.Map<Boeking>(boekingVM);
                    await _boekingService.AddAsync(boekingEntity);

                }
                HttpContext.Session.Remove("ShoppingCart");
                return RedirectToAction("Index", "BoekingAfronding");


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return RedirectToAction("ShoppingCartLeegIndex", "BoekingAfronding");

        }






        public IActionResult Delete(int plaatsInShoppingCart)
        {
            try
            {


                ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");//hoofdlettergevoelig
                CartVM? itemToRemove = cartList?.Carts?.ElementAt(plaatsInShoppingCart);
                cartList?.Carts?.RemoveAt(plaatsInShoppingCart);

                if (itemToRemove != null)
                {
                    HttpContext.Session.Remove("ShoppingCart");
                    HttpContext.Session.SetObject("ShoppingCart", cartList);
                }
                return View("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View("index");

        }
    }
}
