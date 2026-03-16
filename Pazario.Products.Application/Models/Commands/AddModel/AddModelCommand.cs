using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Models.Commands.AddModel
{
    public record AddModelCommand : ICommand
    {
        public required string Name { get; init; }
        public required Guid MarkaId { get; init; }
    }
}
