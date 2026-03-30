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
        public Category()
        {
            Properties = new HashSet<CategoryProperty>();
            Children = new HashSet<Category>();
        }
        public Name Name { get; set; }
        public Guid? ParentId { get; set; }
        public Category Parent { get; set; }
        public Icon? Icon { get; set; }
        public HashSet<Category> Children { get; set; }
        public HashSet<CategoryProperty> Properties { get; set; }
    }
}
