using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pazario.Products.Application.Markas.Commands.AddMarka;
using Pazario.Products.Application.Markas.Commands.RemoveMarka;
using Pazario.Products.Application.Markas.Commands.UpdateMarka;
using Pazario.Products.Application.Markas.Queries.GetMarka;
using Pazario.Products.Application.Markas.Queries.GetMarkas;
using Pazario.Products.Presentation.Markas.DTOs;

namespace Pazario.Products.Presentation.Markas
{
    public static class MarkaEndpoints
    {
        public static IEndpointRouteBuilder MapMarkaEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/markas");

            group.MapGet("", GetMarkas);
            group.MapGet("{id:guid}", GetMarka);
            group.MapPost("", AddMarka);
            group.MapPut("{id:guid}", UpdateMarka);
            group.MapDelete("{id:guid}", RemoveMarka);

            return app;
        }

        private static async Task<IResult> GetMarkas(
            ISender sender,
            IMapper mapper,
            string key = "",
            CancellationToken cancellationToken = default)
        {
            var query = new GetMarkasQuery { Key = key };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<MarkaResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetMarka(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var query = new GetMarkaQuery { Id = id };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<MarkaResponseDto>(result.Value));
        }

        private static async Task<IResult> AddMarka(
            ISender sender,
            IMapper mapper,
            MarkaRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(mapper.Map<AddMarkaCommand>(request), cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateMarka(
            Guid id,
            ISender sender,
            IMapper mapper,
            MarkaRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateMarkaCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveMarka(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveMarkaCommand { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
