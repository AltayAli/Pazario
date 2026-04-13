using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pazario.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty;
using Pazario.Products.Application.CategoryProperties.Queries.GetCategoryProperties;
using Pazario.Products.Application.CategoryPropertyValues.UpdateCategoryPropertyValues;
using Pazario.Products.Presentation.CategoryProperties.DTOs;

namespace Pazario.Products.Presentation.CategoryProperties
{
    public static class CategoryPropertyEndpoints
    {
        public static IEndpointRouteBuilder MapCategoryPropertyEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/categories/{categoryId:guid}/properties");

            group.MapGet("", GetCategoryProperties);
            group.MapPut("", SaveCategoryProperties);

            return app;
        }

        private static async Task<IResult> GetCategoryProperties(
            Guid categoryId,
            ISender sender,
            IMapper mapper,
            string key = "",
            CancellationToken cancellationToken = default)
        {
            var query = new GetCategoryPropertiesQuery { CategoryId = categoryId, Key = key };
            var result = await sender.Send(query, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<CategoryPropertyResponseDto>>(result.Value));
        }

        private static async Task<IResult> SaveCategoryProperties(
            Guid categoryId,
            ISender sender,
            IMapper mapper,
            SaveCategoryPropertiesRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var propertyResult = await sender.Send(new UpdateCategoryPropertyCommand
            {
                CategoryId = categoryId,
                Items = mapper.Map<List<UpdateCategoryPropertyCommandItem>>(request.Items)
            }, cancellationToken);

            if (propertyResult.IsFailure)
                return Results.BadRequest(propertyResult.Error);

            var propertyIds = propertyResult.Value;

            for (int i = 0; i < request.Items.Count; i++)
            {
                var valuesResult = await sender.Send(new UpdateCategoryPropertyValuesCommand
                {
                    PropertyId = propertyIds[i],
                    Items = mapper.Map<List<UpdateCategoryPropertyValuesCommandItem>>(request.Items[i].Values)
                }, cancellationToken);

                if (valuesResult.IsFailure)
                    return Results.BadRequest(valuesResult.Error);
            }

            return Results.NoContent();
        }
    }
}
