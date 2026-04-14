using Pazario.Common.Domain.Abstractions;

namespace Pazario.Common.Domain.ValueObjects
{
    public record Name : ValueObject
    {
        public string Value { get; }
        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.", nameof(value));
            Value = value;
        }

        public static implicit operator string(Name n) => n?.Value;
        public static explicit operator Name(string n) => new Name(n);
        public override string ToString() => Value;
        public string ToNormalizedString() => Value.Trim().ToLower();
    }
}
