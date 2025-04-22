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
        public TicketController(IMapper mapper, IReisklasseService reisklasseService, IMaaltijdService maaltijdService)
        {
            _mapper = mapper;
            this._reisklasseService = reisklasseService;
            this._maaltijdService = maaltijdService;
        }
        public async IActionResult Index(VluchtVM vluchtVM)
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



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }

            return View();
        }
    }
}
