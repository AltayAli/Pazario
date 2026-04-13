using AutoMapper;
using Pazario.Products.Application.Categories.Commands.AddCategory;
using Pazario.Products.Application.Categories.Commands.UpdateCategory;
using Pazario.Products.Application.Categories.Queries.GetCategories;
using Pazario.Products.Presentation.Categories.DTOs;

namespace Pazario.Products.Presentation.Categories.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<AddCategoryRequestDto, AddCategoryCommand>();
            CreateMap<UpdateCategoryRequestDto, UpdateCategoryCommand>();
            CreateMap<GetCategoriesItemResponse, CategoryResponseDto>();
        }
    }
}
