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
            return View("Index");
        }
    }
}
