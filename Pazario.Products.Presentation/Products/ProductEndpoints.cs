using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pazario.Products.Application.Products.Commands.CreateProduct;
using Pazario.Products.Application.Products.Commands.RemoveProduct;
using Pazario.Products.Application.Products.Commands.UpdateProduct;
using Pazario.Products.Application.Products.Queries.GetProduct;
using Pazario.Products.Application.Products.Queries.GetProducts;
using Pazario.Products.Presentation.Products.DTOs;

namespace Pazario.Products.Presentation.Products
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/products");

            group.MapGet("", GetProducts);
            group.MapGet("{id:guid}", GetProduct);
            group.MapPost("", AddProduct);
            group.MapPut("{id:guid}", UpdateProduct);
            group.MapDelete("{id:guid}", RemoveProduct);

            return app;
        }

        private static async Task<IResult> GetProducts(
            ISender sender,
            IMapper mapper,
            string key = "",
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetProductsQuery { Key = key }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(mapper.Map<List<ProductListResponseDto>>(result.Value));
        }

        private static async Task<IResult> GetProduct(
            Guid id,
            ISender sender,
            IMapper mapper,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetProductQuery { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(mapper.Map<ProductDetailResponseDto>(result.Value));
        }

        private static async Task<IResult> AddProduct(
            ISender sender,
            IMapper mapper,
            AddProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(mapper.Map<CreateProductCommand>(request), cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created($"api/products/{result.Value}", result.Value);
        }

        private static async Task<IResult> UpdateProduct(
            Guid id,
            ISender sender,
            IMapper mapper,
            UpdateProductRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(
                mapper.Map<UpdateProductCommand>(request) with { Id = id },
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveProduct(
            Guid id,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductCommand { Id = id }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
