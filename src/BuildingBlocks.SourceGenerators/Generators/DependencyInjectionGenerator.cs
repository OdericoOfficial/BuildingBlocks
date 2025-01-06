﻿using System.Collections.Immutable;
using System.Text;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.SourceGenerators.Generators
{
    internal static class DependencyInjectionGenerator
    {
        public static void RegisterServices(SourceProductionContext context, (ImmutableArray<DependencyInjectionSource> Left, ImmutableArray<DependencyInjectionSource> Right) sources)
        {
            var sourceBuilder = new StringBuilder();
            sourceBuilder.AppendLine(@"// <auto-generated />

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddRegisteredServices(this IServiceCollection services)
        {");
            RegisterServices(sourceBuilder, sources.Left);
            RegisterServices(sourceBuilder, sources.Right);
            sourceBuilder.AppendLine(@"            return services;
        }
    }
}");
            context.AddSource("IServiceCollectionExtensions.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
        }

        private static void RegisterServices(StringBuilder sourceBuilder, ImmutableArray<DependencyInjectionSource> sources)
        {
            foreach (var source in sources)
            {
                if (source.ServiceName == string.Empty)
                    continue;

                var expression = string.Empty;

                if (source.IsHosted)
                    expression = $"            services.AddHostedService<{source.ServiceName}>();";
                else if (source.IsEnumerable && source.ImplementationName != string.Empty)
                    expression = source.Key == string.Empty ? GetEnumerableServiceExpression(source) : GetKeyedEnumerableServiceExpression(source);
                else
                    expression = source.Key == string.Empty ? GetServiceExpression(source) : GetKeyedServiceExpression(source);

                if (expression != string.Empty)
                    sourceBuilder.AppendLine(expression);
            }
        }

        private static string GetKeyedEnumerableServiceExpression(DependencyInjectionSource source)
            => source.Lifetime switch
            {
                ServiceLifetime.Scoped => $"            services.TryAddEnumerable(ServiceDescriptor.KeyedScoped(typeof({source.ServiceName}), {source.Key}, typeof({source.ImplementationName})));",
                ServiceLifetime.Singleton => $"            services.TryAddEnumerable(typeof({source.ServiceName}), {source.Key}, typeof({source.ImplementationName})));",
                _ => $"            services.TryAddEnumerable(ServiceDescriptor.KeyedTransient(typeof({source.ServiceName}), {source.Key}, typeof({source.ImplementationName})));"
            };

        private static string GetEnumerableServiceExpression(DependencyInjectionSource source)
            => source.Lifetime switch
            {
                ServiceLifetime.Scoped => $"            services.TryAddEnumerable(ServiceDescriptor.Scoped(typeof({source.ServiceName}), typeof({source.ImplementationName})));",
                ServiceLifetime.Singleton => $"            services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof({source.ServiceName}), typeof({source.ImplementationName})));",
                _ => $"            services.TryAddEnumerable(ServiceDescriptor.Transient(typeof({source.ServiceName}), typeof({source.ImplementationName})));"
            };

        private static string GetKeyedServiceExpression(DependencyInjectionSource source)
            => source.Lifetime switch
            {
                ServiceLifetime.Scoped => source.ImplementationName != string.Empty ?
                $"            services.TryAddKeyedScoped(typeof({source.ServiceName}), {source.Key}, typeof({source.ImplementationName}));"
                    : $"            services.TryAddKeyedScoped(typeof({source.ServiceName}), {source.Key});",
                ServiceLifetime.Singleton => source.ImplementationName != string.Empty ?
                $"            services.TryAddKeyedSingleton(typeof({source.ServiceName}), {source.Key}, typeof({source.ImplementationName}));"
                    : $"            services.TryAddKeyedSingleton(typeof({source.ServiceName}), {source.Key});",
                _ => source.ImplementationName != string.Empty ?
                $"            services.TryAddKeyedTransient(typeof({source.ServiceName}), {source.Key}, typeof({source.ImplementationName}));"
                    : $"            services.TryAddKeyedTransient(typeof({source.ServiceName}), {source.Key});"
            };

        private static string GetServiceExpression(DependencyInjectionSource source)
            => source.Lifetime switch
            {
                ServiceLifetime.Scoped => source.ImplementationName != string.Empty ?
                $"            services.TryAddScoped(typeof({source.ServiceName}), typeof({source.ImplementationName}));"
                    : $"            services.TryAddScoped(typeof({source.ServiceName}));",
                ServiceLifetime.Singleton => source.ImplementationName != string.Empty ?
                $"            services.TryAddSingleton(typeof({source.ServiceName}), typeof({source.ImplementationName}));"
                    : $"            services.TryAddSingleton(typeof({source.ServiceName}));",
                _ => source.ImplementationName != string.Empty ?
                $"            services.TryAddTransient(typeof({source.ServiceName}), typeof({source.ImplementationName}));"
                    : $"            services.TryAddTransient(typeof({source.ServiceName}));"
            };
    }
}