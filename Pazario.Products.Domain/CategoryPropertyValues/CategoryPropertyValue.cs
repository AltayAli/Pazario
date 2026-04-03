using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;

namespace Pazario.Products.Domain.CategoryPropertyValues
{
    public class CategoryPropertyValue : BaseEntity
    {
        private CategoryPropertyValue()
        {
        }
        public Guid CategoryPropertyId { get; private set; }
        public CategoryProperty CategoryProperty { get; private set; }
        public string Value { get; private set; }

        public static CategoryPropertyValue Create(Guid categoryPropertyId, string value)
        {
            return new CategoryPropertyValue
            {
                CategoryPropertyId = categoryPropertyId,
                Value = value
            };
        }

        public void Update(string value)
        {
            Value = value;
        }
    }
}
