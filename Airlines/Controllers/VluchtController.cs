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
        private readonly IZitplaatsService _zitplaatsService;
        public VluchtController(IPlaatsService plaatsService, IVluchtService vluchtService, IMapper mapper, IZitplaatsService zitplaatsService)
        {
            _plaatsService = plaatsService;
            _vluchtService = vluchtService;
            _mapper = mapper;
            _zitplaatsService = zitplaatsService;
        }
        [HttpPost]
        public async Task<IActionResult> Index(DateTime StartDate, DateTime EndDate, string vertrek, string bestemming)
        {
            try
            {
                Console.WriteLine(vertrek);
                Console.WriteLine(bestemming);
                
                int vertrekPlaatsId = _plaatsService.GetByNaamAsync(vertrek).Result.PlaatsId;
                int bestemmingId = _plaatsService.GetByNaamAsync(bestemming).Result.PlaatsId;

                var lstVluchten = await _vluchtService.GetNormaleVluchtenTussenPlaatsenTussenDatums(vertrekPlaatsId, bestemmingId, StartDate, EndDate);

                
                List<VluchtVM> lstVluchtVM = null;
                if(lstVluchten != null)
                {
                    lstVluchtVM = _mapper.Map<List<VluchtVM>>(lstVluchten);

                    List<VluchtMetOverstappenVM> lstVluchtMetOverstappenVM = new List<VluchtMetOverstappenVM>();
                    foreach (VluchtVM vlucht in lstVluchtVM)
                    {
                       VluchtMetOverstappenVM vluchtMetOverstappenVM = new VluchtMetOverstappenVM();
                        vluchtMetOverstappenVM.GewoneVlucht = vlucht;
                        var lstVanOverstapVluchten = await _vluchtService.GetOverstappenVanVlucht(vlucht.VluchtId);
                        vluchtMetOverstappenVM.OverstapVluchten = _mapper.Map<List<VluchtVM>>(lstVanOverstapVluchten);
                        var aantalBeschikbareZitplaatsen = await _zitplaatsService.TelAantalBeschikbareZitplaatsenVoorVlucht(vlucht.VluchtId);

                       vluchtMetOverstappenVM.AantalBeschikbarePlaatsen = aantalBeschikbareZitplaatsen;

                        lstVluchtMetOverstappenVM.Add(vluchtMetOverstappenVM);  
                    }



                    return View(lstVluchtMetOverstappenVM);

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
