using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Airlines.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LuchthavensController : Controller
    {
        private IService<Plaats> _plaatsService;
        private readonly IMapper _mapper;
        public LuchthavensController(IService<Plaats> plaatsService, IMapper mapper)
        {
            _plaatsService = plaatsService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PlaatsVM>> Get()
        {
            return View();
        }
    }
}
