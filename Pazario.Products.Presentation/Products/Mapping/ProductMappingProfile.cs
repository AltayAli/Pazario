using AutoMapper;
using Pazario.Products.Application.Products.Commands.CreateProduct;
using Pazario.Products.Application.Products.Commands.UpdateProduct;
using Pazario.Products.Application.Products.Queries.GetProduct;
using Pazario.Products.Application.Products.Queries.GetProducts;
using Pazario.Products.Application.ProductVariants.Commands.AddProductVariant;
using Pazario.Products.Application.ProductVariants.Commands.UpdateProductVariant;
using Pazario.Products.Presentation.Products.DTOs;

namespace Pazario.Products.Presentation.Products.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // Product
            CreateMap<AddProductRequestDto, CreateProductCommand>();
            CreateMap<UpdateProductRequestDto, UpdateProductCommand>();

            // Variant
            CreateMap<AddProductVariantRequestDto, AddProductVariantCommand>();
            CreateMap<AddProductVariantImageItemDto, AddProductVariantImageItem>();
            CreateMap<AddProductVariantPropertyItemDto, AddProductVariantPropertyItem>();

            CreateMap<UpdateProductVariantRequestDto, UpdateProductVariantCommand>();
            CreateMap<UpdateProductVariantImageItemDto, UpdateProductVariantImageItem>();
            CreateMap<UpdateProductVariantPropertyItemDto, UpdateProductVariantPropertyItem>();

            // Queries
            CreateMap<GetProductsItemResponse, ProductListResponseDto>();
            CreateMap<GetProductResponse, ProductDetailResponseDto>();
            CreateMap<GetProductCategoryItem, ProductCategoryItemDto>();
            CreateMap<GetProductVariantItem, ProductVariantItemDto>();
            CreateMap<GetProductVariantImageItem, ProductVariantImageItemDto>();
            CreateMap<GetProductVariantPropertyItem, ProductVariantPropertyItemDto>();
        }
    }
}
