using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model;
using System.Xml.Linq;

namespace MyTaskAPI.Controllers.Auth
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class GetPassHASHController : ControllerBase
    {

        private readonly ILogger<LoginController> _logger;
        public readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _config;



        public GetPassHASHController(
                                IConfiguration config,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<ApplicationRole> roleManager,
                                IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email, string password)
        {
#if DEBUG
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {                        
                user.PasswordHash = _passwordHasher.HashPassword(user, password);

                return Ok(user.PasswordHash);
            }
            
            return BadRequest("Пользователя не обнаружено");

#else
    
            return Ok();

#endif



        }

    }
}
