using AutoMapper;

namespace Movington.Features.Categories
{
    internal sealed class CategoriesMappingProfile : Profile
    {
        public CategoriesMappingProfile()
        {
            CreateMap<Category, CategoryDetailsViewModel>();
            CreateMap<CategoryEditViewModel, Category>();
        }
    }
}