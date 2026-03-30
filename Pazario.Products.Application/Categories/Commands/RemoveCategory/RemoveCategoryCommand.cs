using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Categories.Commands.RemoveCategory
{
    public record RemoveCategoryCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
