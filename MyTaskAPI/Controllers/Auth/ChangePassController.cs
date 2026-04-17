using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model;
using MyTaskAPI.Model.Auth;

namespace MyTaskAPI.Controllers.Auth
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePassController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginController> _logger;
        public readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly UserManager<ApplicationUser> _userManager;


        public ChangePassController(SignInManager<ApplicationUser> signInManager, 
            ILogger<LoginController> logger,
            UserManager<ApplicationUser> userManager,
            IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> change(ChangePassDTO changePass )
        {
            var s = HttpContext.User;
            var u = new UserDTO();

            if (u != null)
            {
                u.name = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Name;
                u.email = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                u.roles = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims.Where(i => i.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(i => i.Value).ToList();
            }

            var user = await _userManager.FindByEmailAsync(u.email);

            if (user == null) {
                throw new HttpRequestException("User not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePass.oldPass, changePass.newPass);

            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors.First().Description);               
            }
        }

    }
}
