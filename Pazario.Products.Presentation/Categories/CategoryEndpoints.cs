using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pazario.Products.Application.Categories.Commands.AddCategory;
using Pazario.Products.Application.Categories.Commands.RemoveCategory;
using Pazario.Products.Application.Categories.Commands.UpdateCategory;
using Pazario.Products.Application.Categories.Queries.GetCategories;
using Pazario.Products.Presentation.Categories.DTOs;

namespace Pazario.Products.Presentation.Categories
{
    public static class CategoryEndpoints
    {
        public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/categories");

            group.MapGet("", GetCategories);
            group.MapPost("", AddCategory);
            group.MapPut("{id:guid}", UpdateCategory);
            group.MapDelete("{id:guid}", RemoveCategory);

            return app;
        }

        private static async Task<IResult> GetCategories(
            ISender sender,
            IMapper mapper,
            string key = "",
            Guid? parentId = null,
            CancellationToken cancellationToken = default)
        {
            var query = new GetCategoriesQuery { Key = key, ParentId = parentId };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<CategoryResponseDto>>(result.Value));
        }

        private static async Task<IResult> AddCategory(
            ISender sender,
            IMapper mapper,
            AddCategoryRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<AddCategoryCommand>(request);
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok();
        }

        private static async Task<IResult> UpdateCategory(
            Guid id,
            ISender sender,
            IMapper mapper,
            UpdateCategoryRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var command = mapper.Map<UpdateCategoryCommand>(request) with { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveCategory(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new RemoveCategoryCommand { Id = id };
            var result = await sender.Send(command, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
