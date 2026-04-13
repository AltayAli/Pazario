using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.ProductVariantProperties.Commands.RemoveProductVariantProperty
{
    public record RemoveProductVariantPropertyCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
