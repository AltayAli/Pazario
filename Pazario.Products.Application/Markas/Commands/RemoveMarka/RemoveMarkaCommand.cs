using Pazario.Products.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Markas.Commands.RemoveMarka
{
    public record RemoveMarkaCommand  : ICommand
    {
        public Guid Id { get; set; }
    }
}
