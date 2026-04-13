using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand : ICommand<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ModelId { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
