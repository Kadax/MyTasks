using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTaskAPI.Model;

namespace MyTaskAPI.Controllers.Auth
{


    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
                                                     
        private readonly ILogger<LoginController> _logger;
        public readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        
        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,  IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }

       
        /// <summary>
        /// Функция проверки сущетвования базовых ролей и Администратора системы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> CheckAdmin()
        {
            var ret = "";
            
            var roles = _roleManager.Roles.ToList();

            if(roles.Count < 2)
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

            var user = await _userManager.FindByNameAsync("admin@admin.com");

            if (user == null)
            {
                ret += "Create Admin \n\r";


                user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };

                var password = "admin"; // Change this

                user.PasswordHash = _passwordHasher.HashPassword(user, password);

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
