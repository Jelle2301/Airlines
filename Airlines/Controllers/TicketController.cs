using Airlines.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Airlines.Extensions;
namespace Airlines.Controllers
{
    public class TicketController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReisklasseService _reisklasseService;
        private readonly IMaaltijdService _maaltijdService;
        private readonly ISeizoenService _seizoenService;
        private readonly IVluchtService _vluchtService;
        public TicketController(IMapper mapper, IReisklasseService reisklasseService, IMaaltijdService maaltijdService, ISeizoenService seizoenService, IVluchtService vluchtService)
        {
            _mapper = mapper;
            this._reisklasseService = reisklasseService;
            this._maaltijdService = maaltijdService;
            this._seizoenService = seizoenService;
            this._vluchtService = vluchtService;
        }
        
        public async Task<IActionResult> Index(VluchtVM vluchtVM)
        {

            try
            {
                var lstReisklassen = await _reisklasseService.GetAllAsync();
                var lstMaaltijden = await _maaltijdService.GetAllAsync();
                TicketMogelijkhedenVM ticketMogelijkhedenVM = new TicketMogelijkhedenVM();
                if (lstReisklassen != null)
                {
                    ticketMogelijkhedenVM.Reisklassen = _mapper.Map<List<ReisklasseVM>>(lstReisklassen);
                }
                if (lstMaaltijden != null)
                {
                    ticketMogelijkhedenVM.Maaltijden = _mapper.Map<List<MaaltijdVM>>(lstMaaltijden);
                }
                ticketMogelijkhedenVM.Vlucht = vluchtVM;
                var lstSeizoenen = await _seizoenService.GetAllAsync();
                if (lstSeizoenen != null)
                {
                    //gaat de seizoenen geven die in die perdio liggen
                    var tijdVertrek = vluchtVM.TijdVertrek;
                    var seizoen = lstSeizoenen.FirstOrDefault(s =>
                    {
                        var begin = new DateTime(1, s.BeginDatum.Month, s.BeginDatum.Day);
                        var eind = new DateTime(1, s.EindDatum.Month, s.EindDatum.Day);
                        var vertrek = new DateTime(1, tijdVertrek.Month, tijdVertrek.Day);

                        
                        return vertrek >= begin && vertrek <= eind;
                    });
                    //als geen seizoenen heeft gevonden doet die niets, anders voegt die dat toe aan de VM
                    if (seizoen != null)
                    {
                        ticketMogelijkhedenVM.Seizoen = _mapper.Map<SeizoenVM>(seizoen);
                    }
                }
               ticketMogelijkhedenVM.TotaalPrijs = vluchtVM.BeginPrijs;
                return View(ticketMogelijkhedenVM);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }

            return View();
        }
        
        public async Task<IActionResult> VluchtSelecteren(TicketVM ticketVM, int maaltijdId, int reisklasseId , int vluchtId, int seizoenId)
        {
            try
            {
                var gekozenReisklasse = await _reisklasseService.GetByIdAsync(reisklasseId);
                var gekozenMaaltjd = await _maaltijdService.GetByIdAsync(maaltijdId);
                var vlucht = await _vluchtService.GetByIdAsync(vluchtId);
                if(seizoenId != 0)
                {
                    var seizoen = _seizoenService.GetByIdAsync(seizoenId);
                    ticketVM.Seizoen = _mapper.Map<SeizoenVM>(seizoen);
                }

                ticketVM.Reisklasse = _mapper.Map<ReisklasseVM>(gekozenReisklasse);
                ticketVM.Maaltijd = _mapper.Map<MaaltijdVM>(gekozenMaaltjd);
                ticketVM.Vlucht = _mapper.Map<VluchtVM>(vlucht);

                var shoppingCartVM = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") ?? new ShoppingCartVM { Carts = new List<CartVM>() };

                shoppingCartVM.Carts.Add(new CartVM
                {
                    Ticket = ticketVM
                    
                });

                HttpContext.Session.SetObject("ShoppingCart", shoppingCartVM);
                return RedirectToAction("Index", "ShoppinCart");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
        }
        


    }
}
