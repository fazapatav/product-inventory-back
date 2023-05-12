using Microsoft.EntityFrameworkCore;
using Sofka.ProductInventory.Core.Entities;

namespace Sofka.ProductInventory.Infrastucture.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Product { get; set; }
        public DbSet<Buy> Buy { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
