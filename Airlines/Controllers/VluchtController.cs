using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Services.Interfaces;

namespace Airlines.Controllers
{
    public class VluchtController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlaatsService _plaatsService;
        private readonly IVluchtService _vluchtService;
        public VluchtController(IPlaatsService plaatsService, IVluchtService vluchtService, IMapper mapper)
        {
            _plaatsService = plaatsService;
            _vluchtService = vluchtService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(DateTime StartDate, DateTime EndDate, string vertrek, string bestemming)
        {
            try
            {
                Console.WriteLine(vertrek);
                Console.WriteLine(bestemming);
                
                int vertrekPlaatsId = _plaatsService.GetByNaamAsync(vertrek).Result.PlaatsId;
                int bestemmingId = _plaatsService.GetByNaamAsync(bestemming).Result.PlaatsId;
                var lstVluchten = await _vluchtService.GetVluchtenTussenPlaatsen(vertrekPlaatsId, bestemmingId);
                
                List<VluchtVM> lstVluchtVM = null;
                if(lstVluchten != null)
                {
                    lstVluchtVM = _mapper.Map<List<VluchtVM>>(lstVluchten);
                    return View(lstVluchtVM);
                }
        
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
           
        }
    }
}
