using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movington.Database;

namespace Movington.Features.Categories
{
    public sealed class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasPgGeneratedValueOnAdd();

            builder
                .Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder
                .HasIndex(x => x.Name)
                .IsUnique();
        }
    }
}