using Airlines.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Airlines.Controllers
{
    public class AanbiedBestemmingenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPlaatsService _plaatsService;
        public AanbiedBestemmingenController(IPlaatsService plaatsService, IMapper mapper)
        {
            _plaatsService = plaatsService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var lstPlaatsen = await _plaatsService.GetAllAsync();
                Console.WriteLine(lstPlaatsen);
                List<PlaatsVM> lstPlaatsVM = null;//? of gwn op null zetten
                if (lstPlaatsen != null)
                {
                    Console.WriteLine("test1");
                    lstPlaatsVM = _mapper.Map<List<PlaatsVM>>(lstPlaatsen);
                    return View(lstPlaatsVM);
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
