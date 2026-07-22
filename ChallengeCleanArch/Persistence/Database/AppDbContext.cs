using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Products> Products { get; set; }
    }
}
