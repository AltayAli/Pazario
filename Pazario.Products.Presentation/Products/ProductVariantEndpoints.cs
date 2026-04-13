using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Pazario.Products.Application.ProductVariantImages.Commands.AddProductVariantImages;
using Pazario.Products.Application.ProductVariantImages.Commands.RemoveProductVariantImage;
using Pazario.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties;
using Pazario.Products.Application.ProductVariantProperties.Commands.RemoveProductVariantProperty;
using Pazario.Products.Application.ProductVariants.Commands.AddProductVariant;
using Pazario.Products.Application.ProductVariants.Commands.RemoveProductVariant;
using Pazario.Products.Application.ProductVariants.Commands.UpdateProductVariant;
using Pazario.Products.Presentation.Products.DTOs;
using AppendImageItem = Pazario.Products.Application.ProductVariantImages.Commands.AddProductVariantImages.AddProductVariantImageItem;
using AppendPropertyItem = Pazario.Products.Application.ProductVariantProperties.Commands.AddProductVariantProperties.AddProductVariantPropertyItem;

namespace Pazario.Products.Presentation.Products
{
    public static class ProductVariantEndpoints
    {
        public static IEndpointRouteBuilder MapProductVariantEndpoints(this IEndpointRouteBuilder app)
        {
            var variants = app.MapGroup("api/products/{productId:guid}/variants");

            variants.MapPost("", AddVariant);
            variants.MapPut("{variantId:guid}", UpdateVariant);
            variants.MapDelete("{variantId:guid}", RemoveVariant);

            var images = app.MapGroup("api/variants/{variantId:guid}/images");

            images.MapPost("", AddImages);
            images.MapDelete("{imageId:guid}", RemoveImage);

            var properties = app.MapGroup("api/variants/{variantId:guid}/properties");

            properties.MapPost("", AddProperties);
            properties.MapDelete("{propertyId:guid}", RemoveProperty);

            return app;
        }

        private static async Task<IResult> AddVariant(
            Guid productId,
            ISender sender,
            IMapper mapper,
            AddProductVariantRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(
                mapper.Map<AddProductVariantCommand>(request) with { ProductId = productId },
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created($"api/variants/{result.Value}/images", result.Value);
        }

        private static async Task<IResult> UpdateVariant(
            Guid variantId,
            ISender sender,
            IMapper mapper,
            UpdateProductVariantRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(
                mapper.Map<UpdateProductVariantCommand>(request) with { VariantId = variantId },
                cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveVariant(
            Guid variantId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductVariantCommand { Id = variantId }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> AddImages(
            Guid variantId,
            ISender sender,
            AddProductVariantImagesRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new AddProductVariantImagesCommand
            {
                VariantId = variantId,
                Images = request.Images.Select(i => new AppendImageItem
                {
                    ImageUrl = i.ImageUrl,
                    IsMain = i.IsMain
                }).ToList()
            }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveImage(
            Guid imageId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductVariantImageCommand { Id = imageId }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> AddProperties(
            Guid variantId,
            ISender sender,
            AddProductVariantPropertiesRequestDto request,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new AddProductVariantPropertiesCommand
            {
                VariantId = variantId,
                Properties = request.Properties.Select(p => new AppendPropertyItem
                {
                    CategoryPropertyId = p.CategoryPropertyId,
                    Value = p.Value
                }).ToList()
            }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }

        private static async Task<IResult> RemoveProperty(
            Guid propertyId,
            ISender sender,
            CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new RemoveProductVariantPropertyCommand { Id = propertyId }, cancellationToken);

            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.NoContent();
        }
    }
}
