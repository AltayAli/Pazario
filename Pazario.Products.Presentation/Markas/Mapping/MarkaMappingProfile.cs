using AutoMapper;
using Pazario.Products.Application.Markas.Commands.AddMarka;
using Pazario.Products.Application.Markas.Commands.UpdateMarka;
using Pazario.Products.Application.Markas.Queries.GetMarka;
using Pazario.Products.Application.Markas.Queries.GetMarkas;
using Pazario.Products.Presentation.Markas.DTOs;

namespace Pazario.Products.Presentation.Markas.Mapping
{
    public class MarkaMappingProfile : Profile
    {
        public MarkaMappingProfile()
        {
            CreateMap<MarkaRequestDto, AddMarkaCommand>();
            CreateMap<MarkaRequestDto, UpdateMarkaCommand>();
            CreateMap<GetMarkasResponse, MarkaResponseDto>();
            CreateMap<GetMarkaResponse, MarkaResponseDto>();
        }
    }
}
