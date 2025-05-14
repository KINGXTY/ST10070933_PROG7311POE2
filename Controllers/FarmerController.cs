using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10070933PROG7311.DAL;
using ST10070933PROG7311.Models;

namespace ST10070933PROG7311.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly AgriDbContext _context;

        public FarmerController(AgriDbContext context)
        {
            _context = context;
        }

        // Shows products added by the currently logged-in farmer
        public IActionResult MyProducts()
        {
            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == user.Id);

            if (farmer == null) return NotFound("Farmer profile not found.");

            var products = _context.Products.Where(p => p.FarmerId == farmer.FarmerId).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            // First get the current farmer
            var username = User.Identity.Name;
            var farmer = _context.Farmers.FirstOrDefault(f => f.User.Username == username);

            if (farmer == null)
            {
                TempData["errorMessage"] = "You need a farmer profile to add products.";
                return View(product);
            }

            // Set the relationship before validation
            product.FarmerId = farmer.FarmerId;
            product.Farmer = farmer; 

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                TempData["successMessage"] = "Product added successfully!";
                return RedirectToAction("MyProducts");
            }

            // Show specific validation errors
            TempData["errorMessage"] = string.Join("<br>",
                ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

            return View(product);
        }
    }

    }
    
