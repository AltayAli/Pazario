using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.ProductVariantImages.Commands.AddProductVariantImages
{
    public record AddProductVariantImagesCommand : ICommand
    {
        public Guid VariantId { get; set; }
        public List<AddProductVariantImageItem> Images { get; set; } = new();
    }

    public record AddProductVariantImageItem
    {
        public required string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }
}
