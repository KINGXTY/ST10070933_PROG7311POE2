using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10070933PROG7311.DAL;
using ST10070933PROG7311.Models;
using Microsoft.EntityFrameworkCore;

namespace ST10070933PROG7311.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly AgriDbContext _context;

        public EmployeeController(AgriDbContext context)
        {
            _context = context;
        }
        // Displays all farmers in the database
        public IActionResult AllFarmers()
        {
            return View(_context.Farmers.ToList());
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFarmer(Farmer farmer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Ensure UserId is null if not provided
                    farmer.UserId = null;

                    _context.Farmers.Add(farmer);
                    _context.SaveChanges();

                    TempData["successMessage"] = "Farmer added successfully!";
                    return RedirectToAction("AllFarmers");
                }

                // Log validation errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                TempData["errorMessage"] = "Invalid data. Please check the form and try again.";
                return View(farmer);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = $"An error occurred: {ex.Message}";
                return View(farmer);
            }
        }
        // Filters products based on category and date range
        public IActionResult FilterProducts(string category, DateTime? fromDate, DateTime? toDate)
        {
            var products = _context.Products.Include(p => p.Farmer).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                products = products.Where(p => p.Category == category);

            if (fromDate.HasValue)
                products = products.Where(p => p.ProductionDate >= fromDate.Value);

            if (toDate.HasValue)
                products = products.Where(p => p.ProductionDate <= toDate.Value);

            return View(products.ToList());
        }
    }
}
