using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<Abstractions.ValidationError> Errors { get; }
        public ValidationException(IEnumerable<Abstractions.ValidationError> errors)
        {
            Errors = errors;
        }
    }
}
