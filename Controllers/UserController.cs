using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ST10070933PROG7311.DAL;
using ST10070933PROG7311.Models;
using System.Security.Claims;

namespace ST10070933PROG7311.Controllers
{
    public class UserController : Controller
    {
        private readonly AgriDbContext _context;

        public UserController(AgriDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            // Redirect authenticated users to dashboard
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoleDashboard();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Prevent duplicate usernames
                if (_context.Users.Any(u => u.Username == user.Username))
                {
                    TempData["errorMessage"] = "Username already exists. Please choose a different username.";
                    return View(user);
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                //Automatically create a Farmer profile if the user is a Farmer
                if (user.Role == "Farmer")
                {
                    var farmerProfile = new Farmer
                    {
                        FirstName = "First", // default or collect in registration
                        LastName = "Last",
                        FarmName = "My Farm",
                        Location = "Unknown",
                        ContactNumber = "0000000000",
                        UserId = user.Id
                    };

                    _context.Farmers.Add(farmerProfile);
                    _context.SaveChanges();
                }

                TempData["successMessage"] = "Registration successful! Please log in.";
                return RedirectToAction("Login");
            }

            TempData["errorMessage"] = "Invalid data. Please try again.";
            return View(user);
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Skip login page if already logged in
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoleDashboard();
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                TempData["errorMessage"] = "Username and password cannot be empty.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Setup authentication claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    });

                TempData["successMessage"] = $"Welcome, {user.Username}!";
                return RedirectToRoleDashboard(user.Role);
            }

            TempData["errorMessage"] = "Invalid credentials. Please try again.";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Redirect user based on their role
        private IActionResult RedirectToRoleDashboard(string? role = null)
        {
            role ??= User.FindFirst(ClaimTypes.Role)?.Value;

            return role switch
            {
                "Farmer" => RedirectToAction("MyProducts", "Farmer"),
                "Employee" => RedirectToAction("AllFarmers", "Employee"),
                _ => RedirectToAction("Index", "Home")
            };
        }
    }
}
