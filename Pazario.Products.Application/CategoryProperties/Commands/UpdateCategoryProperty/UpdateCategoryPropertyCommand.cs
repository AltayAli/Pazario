using Pazario.Products.Application.Abstractions.Messaging;
using Pazario.Products.Domain.CategoryProperties;

namespace Pazario.Products.Application.CategoryProperties.Commands.UpdateCategoryProperty
{
    public record UpdateCategoryPropertyCommand : ICommand<List<Guid>>
    {
        public Guid CategoryId { get; set; }
        public List<UpdateCategoryPropertyCommandItem> Items { get; set; }
    }

    public record UpdateCategoryPropertyCommandItem
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public CategoryPropertyType Type { get; set; }
        public bool AddToFilter { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
    }
}
