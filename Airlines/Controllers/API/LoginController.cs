using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Airlines.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        public LoginController(SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<object> Post([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return Ok(appUser); // als je correct bent ingelogd, zal een Jwt-Token worden aangemaakt
            }
            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        public class LoginDto
        {
            [Required]
            public string? Email { get; set; }
            [Required]
            public string? Password { get; set; }
        }

        public class RegisterDto
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(6)]
            public string Password { get; set; }

            [Required]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }
    }
}
