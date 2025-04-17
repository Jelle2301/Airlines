using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Airlines.Controllers
{
    public class BoekingsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<Plaats> _plaatsService;
        private readonly IService<Vlucht> _vluchtService;
        public BoekingsController(IService<Plaats> plaatsService, IService<Vlucht> vluchtService, IMapper mapper)
        {
            _plaatsService = plaatsService;
            _vluchtService = vluchtService;
            _mapper = mapper;
        }
        public IActionResult Index(string vertrek, string bestemming)
        {
            try
            {
                int vertrekPlaatsId = _plaatsService.GetByNaamAsync(vertrek).Result.PlaatsId;
                var lstPlaatsen = _plaatsService.GetAllAsync();
                
                
                    
                return View("Index");
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
           
        }
    }
}
