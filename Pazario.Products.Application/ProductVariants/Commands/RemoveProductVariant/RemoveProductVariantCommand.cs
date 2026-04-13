using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.ProductVariants.Commands.RemoveProductVariant
{
    public record RemoveProductVariantCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
