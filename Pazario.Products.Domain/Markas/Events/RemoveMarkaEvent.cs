using Pazario.Products.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Markas.Events
{
    public record RemoveMarkaEvent(Guid Id) : IDomainEvent;
}
