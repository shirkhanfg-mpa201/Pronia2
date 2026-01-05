using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pronia2.Contexts;
using Pronia2.ViewModels.UserViewModels;
using System.Threading.Tasks;

namespace Pronia2.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager,SignInManager<AppUser> _signInManager) : Controller
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


            return Ok("ok");
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
            if (user != null) {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(vm);
            }

           var result= await _userManager.CheckPasswordAsync(user, vm.Password);

            if (!result)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(vm);
            }

          await  _signInManager.SignInAsync(user, vm.RememberMe);

            return Ok("ok");
        } 


        public async Task<IActionResult> Logout()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}