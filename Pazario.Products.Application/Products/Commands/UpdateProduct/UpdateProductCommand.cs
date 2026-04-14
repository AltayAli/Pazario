using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ModelId { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
