using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Products.Commands.AddProduct
{
    public record AddProductCommand : ICommand
    {
        public string Name { get; set; }
        public Guid? ModelId { get; set; }
        public string Description { get; set; }
    }
}
