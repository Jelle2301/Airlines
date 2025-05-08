using System.Security.Claims;
using Airlines.Extensions;
using Airlines.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;


namespace Airlines.Controllers
{
    public class RecenteBoekingenController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBoekingService _boekingService;

        public RecenteBoekingenController(IMapper mapper, IBoekingService boekingService)
        {
            _mapper = mapper;
            _boekingService = boekingService;

        }

        public async Task<IActionResult> Index()
        {
            try
            {

                    var boekingenVanLoggedInPersoon = await _boekingService.GetAllBoekingenVanUser(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    return View(boekingenVanLoggedInPersoon);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
        }
    }
}
