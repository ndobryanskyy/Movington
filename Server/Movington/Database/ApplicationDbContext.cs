using Microsoft.EntityFrameworkCore;
using Movington.Features.Categories;

namespace Movington.Database
{
    public abstract class ApplicationDbContext : DbContext
    {
        protected ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}