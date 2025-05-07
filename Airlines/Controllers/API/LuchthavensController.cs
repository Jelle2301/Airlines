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
        private IPlaatsService _plaatsService;
        private readonly IMapper _mapper;
        public LuchthavensController(IPlaatsService plaatsService, IMapper mapper)
        {
            _plaatsService = plaatsService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<PlaatsVM>> GetAllLuchthavens()
        {
            var luchthavens = await _plaatsService.GetAllAsync();
            if (luchthavens == null)
            {
                return NotFound("No airports found");
            }
            var luchthavensVMs = _mapper.Map<IEnumerable<PlaatsVM>>(luchthavens);
            return Ok(luchthavensVMs);
        }
    }
}
