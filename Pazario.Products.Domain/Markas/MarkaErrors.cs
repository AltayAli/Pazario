using Pazario.Common.Domain.Abstractions;

namespace Pazario.Products.Domain.Markas
{
    public static class MarkaErrors
    {
        public static Error NullValue => new Error("Marka.NullValue", "Marka.NullValue");
        public static Error NotFound => new Error("Marka.NotFound", "Marka.NotFound");
        public static Error MaxLenght => new Error("Marka.MaxLenght", "Marka.MaxLenght");
        public static Error AlreadyExists => new Error("Marka.AlreadyExists", "Marka.AlreadyExists");
    }
}
