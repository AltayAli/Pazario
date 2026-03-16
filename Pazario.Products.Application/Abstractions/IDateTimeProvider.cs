using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Abstractions
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
