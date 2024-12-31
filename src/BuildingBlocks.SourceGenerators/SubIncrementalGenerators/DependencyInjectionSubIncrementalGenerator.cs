﻿using System.Collections.Immutable;
using System.Text;
using BuildingBlocks.SourceGenerators.Abstractions;
using BuildingBlocks.SourceGenerators.Sources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace BuildingBlocks.SourceGenerators.SubIncrementalGenerators
{
    internal class DependencyInjectionSubIncrementalGenerator : SubIncrementalGenerator<DependencyInjectionSubIncrementalGenerator, DependencyInjectionSource>
    {
        public override void RegisterOutput(SourceProductionContext context, ImmutableArray<DependencyInjectionSource> sources)
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"// <auto-generated />");

            var set = new HashSet<string>();
            foreach (var item in sources)
            {
                if (item.ServiceNamespace != string.Empty
                    && !set.Contains(item.ServiceNamespace))
                {
                    builder.AppendLine($"using {item.ServiceNamespace};");
                    set.Add(item.ServiceNamespace);
                }

                if (item.ImplementationNamespace != string.Empty
                    && !set.Contains(item.ImplementationNamespace))
                {
                    builder.AppendLine($"using {item.ImplementationNamespace};");
                    set.Add(item.ImplementationNamespace);
                }
            }

            builder.AppendLine(@"using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class IServiceCollectionExtensions
    {
        internal static IServiceCollection AddRegisteredServices(this IServiceCollection services)
        {");
            foreach (var item in sources)
            {
                builder.AppendLine(item.InjectType switch 
                {
                    InjectType.HostedService => $"            services.AddHostedService<{item.ServiceName}>();",
                    InjectType.Scoped => item.ImplementationName != string.Empty ? 
                        $"            services.TryAddScoped<{item.ServiceName}, {item.ImplementationName}>();" 
                        : $"            services.TryAddScoped<{item.ServiceName}>();",
                    InjectType.Singleton => item.ImplementationName != string.Empty ?
                        $"            services.TryAddSingleton<{item.ServiceName}, {item.ImplementationName}>();"
                        : $"            services.TryAddSingleton<{item.ServiceName}>();",
                    InjectType.Transient => item.ImplementationName != string.Empty ?
                        $"            services.TryAddTransient<{item.ServiceName}, {item.ImplementationName}>();"
                        : $"            services.TryAddTransient<{item.ServiceName}>();",
                    InjectType.KeyedScoped => item.ImplementationName != string.Empty ?
                        $"            services.TryAddKeyedScoped<{item.ServiceName}, {item.ImplementationName}>({item.Key});"
                        : $"            services.TryAddKeyedScoped<{item.ServiceName}>({item.Key});",
                    InjectType.KeyedSingleton => item.ImplementationName != string.Empty ?
                        $"            services.TryAddKeyedSingleton<{item.ServiceName}, {item.ImplementationName}>({item.Key});"
                        : $"            services.TryAddKeyedSingleton<{item.ServiceName}>({item.Key});",
                    InjectType.KeyedTransient => item.ImplementationName != string.Empty ?
                        $"            services.TryAddKeyedTransient<{item.ServiceName}, {item.ImplementationName}>({item.Key});"
                        : $"            services.TryAddKeyedTransient<{item.ServiceName}>({item.Key});",
                    InjectType.EnumerableScoped => $"            services.TryAddEnumerable(ServiceDescriptor.Scoped<{item.ServiceName}, {item.ImplementationName}>());",
                    InjectType.EnumerableSingleton => $"            services.TryAddEnumerable(ServiceDescriptor.Singleton<{item.ServiceName}, {item.ImplementationName}>());",
                    InjectType.EnumerableTransient => $"            services.TryAddEnumerable(ServiceDescriptor.Transient<{item.ServiceName}, {item.ImplementationName}>());",
                    InjectType.KeyedEnumerableScoped => $"            services.TryAddEnumerable(ServiceDescriptor.KeyedScoped<{item.ServiceName}, {item.ImplementationName}>({item.Key}));",
                    InjectType.KeyedEnumerableSingleton => $"            services.TryAddEnumerable(ServiceDescriptor.KeyedSingleton<{item.ServiceName}, {item.ImplementationName}>({item.Key}));",
                    InjectType.KeyedEnumerableTransient => $"            services.TryAddEnumerable(ServiceDescriptor.KeyedTransient<{item.ServiceName}, {item.ImplementationName}>({item.Key}));",
                    _ => string.Empty
                });
            }
            builder.AppendLine(@"            return services;
        }
    }
}");
            context.AddSource("IServiceCollectionExtensions.g.cs", SourceText.From(builder.ToString(), Encoding.UTF8));
        }
    }
}