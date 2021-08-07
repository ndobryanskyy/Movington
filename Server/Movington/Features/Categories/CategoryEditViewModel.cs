using System.ComponentModel.DataAnnotations;

namespace Movington.Features.Categories
{
    public sealed class CategoryEditViewModel
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = default!;
    }
}