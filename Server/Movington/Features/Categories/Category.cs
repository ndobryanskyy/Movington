using System;

namespace Movington.Features.Categories
{
    public sealed class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
    }
}