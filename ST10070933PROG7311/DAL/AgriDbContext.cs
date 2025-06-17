using Microsoft.EntityFrameworkCore;
using ST10070933PROG7311.Models;

namespace ST10070933PROG7311.DAL
{
    public class AgriDbContext : DbContext
    {
        public AgriDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
