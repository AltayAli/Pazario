using Pazario.Products.Domain.Abstractions;
using Pazario.Products.Domain.CategoryProperties;
using Pazario.Products.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Categories
{
    public class Category : BaseEntity
    {
        private Category()
        {
            Properties = new HashSet<CategoryProperty>();
        }
        public Name Name { get; private set; }
        public long ParentId { get; private set; }
        public Category Parent { get; private set; }
        public Icon Icon { get; private set; }
        public HashSet<CategoryProperty> Properties { get; private set; }
    }
}
