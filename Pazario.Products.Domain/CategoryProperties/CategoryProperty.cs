using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.Categories;
using Pazario.Products.Domain.CategoryPropertyValues;
using Pazario.Products.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.CategoryProperties
{
    public class CategoryProperty : BaseEntity
    {
        private CategoryProperty()
        {
           Values = new HashSet<CategoryPropertyValue>();
        }
        public long CategoryId { get; private set; }
        public Category Category { get; private set; }
        public Name Name { get; private set; }
        public CategoryPropertyType Type { get; private set; }
        public bool AddToFilter { get; private set; }
        public bool IsRequired { get; private set; }
        public int DisplayOrder { get; private set; }
        public HashSet<CategoryPropertyValue> Values { get; private set; }
    }
}
