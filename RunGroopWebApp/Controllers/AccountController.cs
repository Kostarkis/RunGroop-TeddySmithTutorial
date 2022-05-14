using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Data;
using RunGroopWebApp.Dtos;
using RunGroopWebApp.Models;

namespace RunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _dbContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }
        public IActionResult Login()
        {
            var response = new LoginAccDto();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginAccDto loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
            }

            TempData["Error"] = "Wrong credentials. Please, try again";
            return View(loginDto);
        }

        public IActionResult Register()
        {
            var response = new RegisterAccDto();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterAccDto registerDto)
        {
            if (!ModelState.IsValid) return View(registerDto);

            var user = await _userManager.FindByEmailAsync(registerDto.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerDto);
            }

            var newUser = new User()
            {
                Email = registerDto.EmailAddress,
                UserName = registerDto.EmailAddress
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerDto.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Index", "Race");
            }
            TempData["Error"] = newUserResponse.Errors.First().Description;
            return View(registerDto);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Race");
        }
    }
}
