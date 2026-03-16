using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Models.Commands.UpdateModel
{
    public record UpdateModelCommand : ICommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required Guid MarkaId { get; init; }
    }
}
