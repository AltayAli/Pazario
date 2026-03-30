using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.CategoryPropertyValues.Commands
{
    public record AddCategoryPropertyValueCommand : ICommand
    {
        public Guid PropertyId { get; set; }
        public List<string> Items { get; init; } = new();
    }
}
