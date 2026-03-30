using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Categories.Commands.AddCategory
{
    public record AddCategoryCommand : ICommand
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public string Icon { get; set; }
    }
   
}
