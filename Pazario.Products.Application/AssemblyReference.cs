using System.Reflection;

namespace Pazario.Products.Application
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;
    }
}
