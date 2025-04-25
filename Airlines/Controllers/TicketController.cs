using Airlines.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Airlines.Controllers
{
    public class TicketController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IReisklasseService _reisklasseService;
        private readonly IMaaltijdService _maaltijdService;
        private readonly ISeizoenService _seizoenService;
        public TicketController(IMapper mapper, IReisklasseService reisklasseService, IMaaltijdService maaltijdService, ISeizoenService seizoenService)
        {
            _mapper = mapper;
            this._reisklasseService = reisklasseService;
            this._maaltijdService = maaltijdService;
            this._seizoenService = seizoenService;
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
        /*
        public Task<IActionResult> VluchtSelecteren(string Voornaam, string Achternaam, int MaaltijdId, int ReisklasseId, int SeizoenId,int VluchtId)
        {
            return View()
        }
        */


    }
}
