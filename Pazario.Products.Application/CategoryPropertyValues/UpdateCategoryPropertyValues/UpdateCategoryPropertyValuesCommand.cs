using Pazario.Products.Application.Abstractions.Messaging;

namespace Pazario.Products.Application.CategoryPropertyValues.UpdateCategoryPropertyValues
{
    public record UpdateCategoryPropertyValuesCommand : ICommand
    {
        public UpdateCategoryPropertyValuesCommand()
        {
            Items = new List<UpdateCategoryPropertyValuesCommandItem>();
        }
        public Guid PropertyId { get; set; }
        public List<UpdateCategoryPropertyValuesCommandItem> Items { get; set; }
    }

    public class UpdateCategoryPropertyValuesCommandItem
    {
        public Guid? Id { get; set; }
        public string Value { get; set; }
    }
}
