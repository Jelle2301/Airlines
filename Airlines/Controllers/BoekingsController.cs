using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Airlines.Controllers
{
    public class BoekingsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlaatsService _plaatsService;
        private readonly IVluchtService _vluchtService;
        public BoekingsController(IPlaatsService plaatsService, IVluchtService vluchtService, IMapper mapper)
        {
            _plaatsService = plaatsService;
            _vluchtService = vluchtService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string vertrek, string bestemming)
        {
            try
            {
                Console.WriteLine(vertrek);
                Console.WriteLine(bestemming);
                
                int vertrekPlaatsId = _plaatsService.GetByNaamAsync(vertrek).Result.PlaatsId;
                int bestemmingId = _plaatsService.GetByNaamAsync(bestemming).Result.PlaatsId;
                var lstVluchten = await _vluchtService.GetVluchtenTussenPlaatsen(vertrekPlaatsId, bestemmingId);

                List<PlaatsVM> lstTestVM = null;

                return View(lstTestVM);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
           
        }
    }
}
