using Airlines.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace Airlines.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class VluchtController : Controller
    {
        private readonly IMapper _mapper;
        private IVluchtService _vluchtService;
        public VluchtController(IMapper mapper, IVluchtService vluchtService)
        {
            _mapper = mapper;
            _vluchtService = vluchtService;
        }
        [HttpGet]
        public async Task<ActionResult<VluchtVM>> GetVluchtenTussenPlaatsen(int vertrekPlaatdId, int bestemmingId)
        {
           var vluchten = await _vluchtService.GetVluchtenTussenPlaatsen(vertrekPlaatdId, bestemmingId);
            if (vluchten == null)
            {
                return NotFound("No flights found");
            }
            var vluchtenVMs = _mapper.Map<IEnumerable<VluchtVM>>(vluchten);
            return Ok(vluchtenVMs);
        }
    }
}
