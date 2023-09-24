using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ankand.Data;
using Ankand.Data.ViewModels;
using Ankand.Models;
using An.Data.Static;

namespace e_Book.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddres);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Produkti");
                    }
                }
                TempData["ErrorMessage"]  = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["ErrorMessage"]  = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(registerVM.FullName==null || registerVM.EmailAddres==null || registerVM.Password==null || registerVM.ConfirmPassword == null)
            {
                TempData["ErrorMessage"] = "AAll fields are required. The password must be at least 6 characters long and include at least one uppercase letter, one lowercase letter, one digit, and one special character.";
                return View(registerVM);
            }
            if (registerVM.Password.Length < 6) 
            {
                TempData["ErrorMessage"] = "Password must be at least 6 characters long.";
                return View(registerVM);
            }
            if (!ModelState.IsValid) return View(registerVM);
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddres);
            if (user != null)
            {
                TempData["ErrorMessage"]= "This email addres is alredy in use.";
                return View(registerVM);
            }
            if(registerVM.Password != registerVM.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Passwords do not match.";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                FullNAme = registerVM.FullName,
                Email = registerVM.EmailAddres,
                UserName = registerVM.EmailAddres
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRole.User);
            }
            return View("RegisterCompleted");
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}

