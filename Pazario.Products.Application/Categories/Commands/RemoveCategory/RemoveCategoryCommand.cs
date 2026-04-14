using Pazario.Common.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.Categories.Commands.RemoveCategory
{
    public record RemoveCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
