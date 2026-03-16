using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Abstractions
{
    public record ValidationError(string Name, string ErrorMessage)
    {
    }
}
