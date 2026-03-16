using Pazario.Products.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pazario.Products.Domain.Models
{
    public class ModelErrors
    {
        public static Error NullValue => new Error("Model.NullValue", "Model.NullValue");
        public static Error NotFound => new Error("Model.NotFound", "Model.NotFound");
        public static Error MaxLenght => new Error("Model.MaxLenght", "Model.MaxLenght");
        public static Error AlreadyExists => new Error("Model.AlreadyExists", "Model.AlreadyExists");
    }
}
