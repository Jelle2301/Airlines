using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Airlines.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private IService<AspNetUser> _userService;
        
        public UserController(IMapper mapper, IService<AspNetUser> userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<UserVM>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();

            if (users == null)
            {
                return NotFound("No users found");
            }

            var userVMs = _mapper.Map<IEnumerable<UserVM>>(users);

            return Ok(userVMs);
        }
    }
}
