using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.CategoryPropertyValues
{
    public class CategoryPropertyValue : BaseEntity
    {
        public Guid CategoryPropertyId { get; set; }
        public CategoryProperty CategoryProperty { get; set; }
        public string Value { get; set; }
    }
}
