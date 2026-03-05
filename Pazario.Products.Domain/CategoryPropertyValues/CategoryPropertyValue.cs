using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.CategoryPropertyValues
{
    public class CategoryPropertyValue : BaseEntity
    {
        private CategoryPropertyValue()
        {
        }
        public long CategoryPropertyId { get; private set; }
        public CategoryProperty CategoryProperty { get; private set; }
        public string Value { get; private set; }
    }
}
