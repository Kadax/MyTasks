using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTaskAPI.Model;

namespace MyTaskAPI.Controllers.Auth
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
                                                     
        private readonly ILogger<LoginController> _logger;
        public readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfiguration _config;



        public AdminController(
                                IConfiguration config,
                                UserManager<ApplicationUser> userManager, 
                                RoleManager<ApplicationRole> roleManager,  
                                IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
            _config = config;
        }


        /// <summary>
        /// Функция проверки сущетвования базовых ролей и Администратора системы
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<string> CheckAdmin()
        {            



            var usersCount = await _userManager.Users.CountAsync();

            if(usersCount != 0)
            {
                throw new Exception("Users already exist in the system");
            }

            var roles = await _roleManager.Roles.ToListAsync();

            var ret = "";

            if (roles.Count < 2)
            {
                ret += "Create base Roles \n\r";

                var admin = await _roleManager.CreateAsync(new ApplicationRole()
                {
                    Id = "Admin",
                    Name = "Administrator",
                    NormalizedName = "admin"
                });

                var userRole = await _roleManager.CreateAsync(new ApplicationRole()
                {
                    Id = "User",
                    Name = "User",
                    NormalizedName = "user"
                });
            }
            else
            {
                ret += "Base Roles exists \n\r";
            }


            var name = _config["DefaultAdmin:Name"];
            var email = _config["DefaultAdmin:Email"];
            var pass = _config["DefaultAdmin:Pass"];

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("an empty user in the configuration");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("an empty email in the configuration");
            }
            if (string.IsNullOrEmpty(pass))
            {
                throw new Exception("an empty pass in the configuration");
            }


            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                ret += "Create Admin \n\r";

                user = new ApplicationUser { UserName = name, Email = email };
                user.PasswordHash = _passwordHasher.HashPassword(user, pass);

                var r2 = await _userManager.CreateAsync(user);
            }
            else
            {
                ret += "User Admin exist\n\r";

                
            }

            var rolesList = await _userManager.GetRolesAsync(user);

            if (!rolesList.Contains("Administrator"))
            {
                var ar= await _userManager.AddToRoleAsync(user, "ADMINISTRATOR");
                ret += "User Admin add role Admin\n\r";
            }
            else
            {
                ret += "User Admin exist role Admin\n\r";
            }

            if (!rolesList.Contains("User"))
            {
                var ar = await _userManager.AddToRoleAsync(user, "USER");
                ret += "User Admin add role User\n\r";
            }
            else
            {
                ret += "User Admin exist role User\n\r";
            }

            return ret;
        }


    }
}
