using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Categories.Commands.AddCategory
{
    public record AddCategoryCommand : ICommand
    {
        public required string Name { get; set; }
        public Guid? ParentId { get; set; }
        public required string Icon { get; set; }
    }
   
}
