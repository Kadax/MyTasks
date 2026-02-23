using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model;
using MyTaskAPI.Model.Auth;

namespace MyTaskAPI.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginController> _logger;
        public readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public LoginController(SignInManager<ApplicationUser> signInManager, ILogger<LoginController> logger, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _signInManager = signInManager;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        [Authorize]
        [HttpGet]
        public async Task<UserDTO> Get()
        {

            var s = HttpContext.User;

            var u = new UserDTO();

            if(u != null)
            {
                u.name = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Name;
                u.email = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                u.roles = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims.Where(i => i.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(i=>i.Value).ToList();
            }

            return u;            

        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<UserDTO> OnPost(SignInDTO signIn)
        {


            var result = await _signInManager.PasswordSignInAsync(signIn.email, signIn.password, signIn.rememberMe, true);

            if (result.Succeeded)
            {
                var u = new UserDTO();

                var s = HttpContext.User;

                if (u != null)
                {
                    u.name = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Name;
                    u.email = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims.FirstOrDefault(i => i.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
                    u.roles = ((System.Security.Claims.ClaimsIdentity)HttpContext.User.Identity).Claims.Where(i => i.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(i => i.Value).ToList();
                }

                return u;
            }
            if (result.IsLockedOut)
            {
                throw new HttpRequestException("IsLockedOut");

            }

            throw new HttpRequestException("error");


        }



    }
}
