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
        
        public async Task<IActionResult> PostData()
        {
            try
            {
                ShoppingCartVM? cartList =
                    HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
                if (cartList == null)
                {
                    return RedirectToAction("Index");
                }
                if(cartList.Carts == null)
                {
                    return RedirectToAction("Index");
                }
                foreach (CartVM cartItem in cartList.Carts)
                {
                    var ticketEntity = _mapper.Map<Ticket>(cartItem.Ticket);
                    await _ticketService.AddAsync(ticketEntity);

                    var boekingVM = new BoekingVM();
                    boekingVM.TicketId =ticketEntity.TicketId;
                    boekingVM.DatumBoeking = DateTime.Now;
                    boekingVM.Status = "Voltooid";
                    boekingVM.TotalePrijs = ticketEntity.Prijs;
                    boekingVM.VoornaamBoeking = ticketEntity.Voornaam;
                    boekingVM.AchternaamBoeking = ticketEntity.Achternaam;
                    boekingVM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var boekingEntity = _mapper.Map<Boeking>(boekingVM);
                    await _boekingService.AddAsync(boekingEntity);

                }


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
