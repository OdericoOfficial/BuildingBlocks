using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection.Attributes;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class AssemblyTargetInjectProvider
    {
        public static IEnumerable<DependencyInjectionSource> Transform(Compilation compilation, CancellationToken cancellationToken)
            => compilation.Assembly.GetAttributes()
                .Where(item => item.AttributeClass is not null
                && (item.AttributeClass.Name == nameof(ScopedAttribute)
                    || item.AttributeClass.Name == nameof(SingletonAttribute)
                    || item.AttributeClass.Name == nameof(TransientAttribute)
                    || item.AttributeClass.Name == nameof(ServiceAttribute)))
                .Select(item => item.AttributeClass!.Name switch
                {
                    nameof(ScopedAttribute) => GetScopedSource(item),
                    nameof(SingletonAttribute) => GetSingletonSource(item),
                    nameof(TransientAttribute) => GetTransientSource(item),
                    _ => GetServiceSource(item)
                });

        private static DependencyInjectionSource GetScopedSource(AttributeData data)
        {

        }

        private static DependencyInjectionSource GetSingletonSource(AttributeData data)
        {

        }

        private static DependencyInjectionSource GetTransientSource(AttributeData data)
        {

        }

        private static DependencyInjectionSource GetServiceSource(AttributeData data)
        {
        }
    }
}
