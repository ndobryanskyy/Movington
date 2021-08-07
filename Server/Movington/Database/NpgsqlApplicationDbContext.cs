using Microsoft.EntityFrameworkCore;

namespace Movington.Database
{
    internal sealed class NpgsqlApplicationDbContext : ApplicationDbContext
    {
        private const string PostgresUuidModuleName = "uuid-ossp";

        public NpgsqlApplicationDbContext(DbContextOptions<NpgsqlApplicationDbContext> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSnakeCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension(PostgresUuidModuleName);
        }
    }
}