using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Commands.UpdateMarka
{
    public record UpdateMarkaCommand : ICommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
