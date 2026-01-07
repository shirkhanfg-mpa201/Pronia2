using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace Pronia2.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<IdentityRole> _roleManager, IConfiguration _configuration) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var existingUser = await _userManager.FindByEmailAsync(vm.EmailAddress);
            if (existingUser != null)
            {
                ModelState.AddModelError("EmailAddress", "Email is already in use.");
                return View(vm);
            }

            var existingUserName = await _userManager.FindByNameAsync(vm.UserName);
            if (existingUserName != null)
            {
                ModelState.AddModelError("UserName", "Username is already in use.");
                return View(vm);
            }

            AppUser appUser = new AppUser()
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.EmailAddress,
                UserName = vm.EmailAddress
            };

            var result = await _userManager.CreateAsync(appUser, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }

            await _signInManager.SignInAsync(appUser, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = await _userManager.FindByEmailAsync(vm.EmailAddress);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(vm);
            }

            var result = await _userManager.CheckPasswordAsync(user, vm.Password);

            if (!result)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(vm);
            }

            await _signInManager.SignInAsync(user, vm.RememberMe);

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> CreateRole()
        {

            await _roleManager.CreateAsync(new IdentityRole()
            {

                Name = "Admin"

            });
            await _roleManager.CreateAsync(new IdentityRole()
            {

                Name = "Member"

            });
            await _roleManager.CreateAsync(new IdentityRole()
            {

                Name = "Moderator"

            });
            
            return Ok("Role Created");

        }

        public async Task<IActionResult> CreateAdminAndModerator()
        {
            var adminUserVM = _configuration.GetSection("AdminUser").Get<UserVm>();
            var moderatorUserVM = _configuration.GetSection("ModeratorUser").Get<UserVm>();

            if (adminUserVM is not null)
            {
                AppUser adminUser = new AppUser()
                {
                    FirstName = adminUserVM.FirstName,
                    LastName = adminUserVM.LastName,
                    Email = adminUserVM.Email,
                    UserName = adminUserVM.UserName,
                };
                await _userManager.CreateAsync(adminUser, adminUserVM.Password);
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }

            if (moderatorUserVM is not null)
            {
                AppUser moderatorUser = new AppUser()
                {
                    FirstName = moderatorUserVM.FirstName,
                    LastName = moderatorUserVM.LastName,
                    Email = moderatorUserVM.Email,
                    UserName = moderatorUserVM.UserName,
                };
                await _userManager.CreateAsync(moderatorUser, moderatorUserVM.Password);
                await _userManager.AddToRoleAsync(moderatorUser, "Moderator");
            }

            return Ok("Successfully!");
        }
    }
}