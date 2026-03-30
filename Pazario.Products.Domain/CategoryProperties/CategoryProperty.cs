using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.CategoryPropertyValues;
using Pazario.Products.Domain.Common;

namespace Pazario.Products.Domain.CategoryProperties
{
    public class CategoryProperty : BaseEntity
    {
        public CategoryProperty()
        {
           Values = new HashSet<CategoryPropertyValue>();
        }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Name Name { get; set; }
        public CategoryPropertyType Type { get; set; }
        public bool AddToFilter { get; set; }
        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public HashSet<CategoryPropertyValue> Values { get; set; }
    }
}
