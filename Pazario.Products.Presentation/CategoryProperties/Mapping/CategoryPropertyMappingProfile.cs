using AutoMapper;
using Pazario.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty;
using Pazario.Products.Application.CategoryProperties.Queries.GetCategoryProperties;
using Pazario.Products.Application.CategoryPropertyValues.UpdateCategoryPropertyValues;
using Pazario.Products.Presentation.CategoryProperties.DTOs;

namespace Pazario.Products.Presentation.CategoryProperties.Mapping
{
    public class CategoryPropertyMappingProfile : Profile
    {
        public CategoryPropertyMappingProfile()
        {
            CreateMap<SaveCategoryPropertyItemDto, UpdateCategoryPropertyCommandItem>();

            CreateMap<SaveCategoryPropertyValueDto, UpdateCategoryPropertyValuesCommandItem>();

            CreateMap<GetCategoryProperiesResponse, CategoryPropertyResponseDto>();
        }
    }
}
