using Pazario.Products.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Categories
{
    public record Icon : ValueObject
    {
        public string Value { get; private set; }
        public Icon(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.", nameof(value));
            Value = value;
        }
    }
}
