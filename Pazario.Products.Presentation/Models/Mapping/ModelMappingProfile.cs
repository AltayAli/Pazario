using AutoMapper;
using Pazario.Products.Application.Models.Commands.AddModel;
using Pazario.Products.Application.Models.Commands.UpdateModel;
using Pazario.Products.Application.Models.Queries.GetModels;
using Pazario.Products.Presentation.Models.DTOs;

namespace Pazario.Products.Presentation.Models.Mapping
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<ModelRequestDto, AddModelCommand>();
            CreateMap<ModelRequestDto, UpdateModelCommand>();
            CreateMap<GetModelsQueryResponse, ModelResponseDto>();
        }
    }
}
