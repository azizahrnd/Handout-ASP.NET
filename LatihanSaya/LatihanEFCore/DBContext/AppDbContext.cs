using Microsoft.EntityFrameworkCore;
using LatihanEFCore.Models;

namespace LatihanEFCore.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
    }
}
