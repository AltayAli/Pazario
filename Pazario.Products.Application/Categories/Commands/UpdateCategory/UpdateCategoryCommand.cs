using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
        public required string Icon { get; set; }
    }
}
