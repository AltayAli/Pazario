using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.ProductVariantImages.Commands.RemoveProductVariantImage
{
    public record RemoveProductVariantImageCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
