using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pazario.Products.Application.Models.Commands.AddModel;
using Pazario.Products.Application.Models.Commands.RemoveModel;
using Pazario.Products.Application.Models.Commands.UpdateModel;
using Pazario.Products.Application.Models.Queries.GetModels;
using Pazario.Products.Presentation.Models.DTOs;

namespace Pazario.Products.Presentation.Models
{
    public static class ModelEndpoints
    {
        public static IEndpointRouteBuilder MapModelEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/markas/{markaId:guid}/models");

            group.MapGet("", GetModels);
            group.MapPost("", AddModel);
            group.MapPut("{id:guid}", UpdateModel);
            group.MapDelete("{id:guid}", RemoveModel);

            return app;
        }

        private static async Task<IResult> GetModels(
            Guid markaId,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetModelsQuery { MarkaId = markaId };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<ModelResponseDto>>(result.Value));
        }

        private static async Task<IResult> AddModel(
            Guid markaId,
            ISender sender,
            IMapper mapper,
            ModelRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddModelCommand>(request) with { MarkaId = markaId };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateModel(
            Guid markaId,
            Guid id,
            ISender sender,
            IMapper mapper,
            ModelRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateModelCommand>(request) with { Id = id, MarkaId = markaId };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveModel(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveModelCommand { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
