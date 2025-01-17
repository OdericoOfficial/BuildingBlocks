using Microsoft.CodeAnalysis;

namespace BuildingBlocks.SourceGenerators.Providers
{
    internal static class AttributeDataExtensions
    {
        public static string GetServiceName(this AttributeData data)
        {
            switch (data.AttributeClass!.TypeArguments.Length)
            {
                case 0:
                    if (data.ConstructorArguments.Length == 1
                        && data.ConstructorArguments[0].Value is INamedTypeSymbol serviceSymbol)
                        return serviceSymbol.ToDisplayString();
                    return string.Empty;
                case 1:
                case 2:
                    return data.AttributeClass.TypeArguments[0].ToDisplayString();
                default:
                    return string.Empty;
            }
        }

        public static string GetServiceName(this AttributeData data, INamedTypeSymbol classSymbol)
        {
            switch (data.AttributeClass!.TypeArguments.Length)
            {
                case 0:
                    if (data.NamedArguments.Any(item => item.Key == "ServiceType")
                        && data.NamedArguments.First(item => item.Key == "ServiceType").Value.Value is INamedTypeSymbol serviceSymbol)
                        return serviceSymbol.ToDisplayString();
                    return classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString();
                case 1:
                    return data.AttributeClass.TypeArguments[0].ToDisplayString();
                default:
                    return string.Empty;
            }
        }

        public static string GetImplementationName(this AttributeData data)
        {
            switch (data.AttributeClass!.TypeArguments.Length)
            {
                case 0:
                case 1:
                    if (data.NamedArguments.Any(item => item.Key == "ImplementationType")
                        && data.NamedArguments.First(item => item.Key == "ImplementationType").Value.Value is INamedTypeSymbol serviceSymbol)
                        return serviceSymbol.ToDisplayString();
                    return string.Empty;
                case 2:
                    return data.AttributeClass.TypeArguments[1].ToDisplayString();
                default:
                    return string.Empty;
            }
        }

        public static string GetImplementationName(this AttributeData data, INamedTypeSymbol classSymbol)
        {
            switch (data.AttributeClass!.TypeArguments.Length)
            {
                case 0:
                    if (data.NamedArguments.Any(item => item.Key == "ServiceType")
                        && data.NamedArguments.First(item => item.Key == "ServiceType").Value.Value is INamedTypeSymbol)
                        return classSymbol.IsGenericType ? classSymbol.ConstructUnboundGenericType().ToDisplayString() : classSymbol.ToDisplayString();
                    return string.Empty;
                case 1:
                    return classSymbol.ToDisplayString();
                default:
                    return string.Empty;
            }
        }
    
        public static string GetKey(this AttributeData data)
        {
            if (data.NamedArguments.Any(item => item.Key == "Key")
                && data.NamedArguments.First(item => item.Key == "Key").Value.Value is string key)
                return $"\"{key}\"";
            return string.Empty;
        }

        public static bool GetIsEnumerableAssembly(this AttributeData data)
        {
            var serviceExist = data.AttributeClass!.TypeArguments.Length switch
            {
                0 => data.ConstructorArguments.Length == 1
                        && data.ConstructorArguments[0].Value is INamedTypeSymbol,
                1 or 2 => true,
                _ => false
            };

            var implementationExist = data.AttributeClass!.TypeArguments.Length switch
            {
                0 or 1 => data.NamedArguments.Any(item => item.Key == "ImplementationType")
                        && data.NamedArguments.First(item => item.Key == "ImplementationType").Value.Value is INamedTypeSymbol,
                2 => true,
                _ => false
            };

            if (serviceExist 
                && implementationExist 
                && data.NamedArguments.Any(item => item.Key == "IsEnumerable")
                && data.NamedArguments.First(item => item.Key == "IsEnumerable").Value.Value is bool isEnumerable)
                return isEnumerable;
            return false;
        }

        public static bool GetIsEnumerableClass(this AttributeData data)
        {
            var serviceExist = data.AttributeClass!.TypeArguments.Length switch
            {
                0 or 1 => true,
                _ => false
            };

            var implementationExist = data.AttributeClass!.TypeArguments.Length switch
            {
                0 => data.NamedArguments.Any(item => item.Key == "ImplementationType")
                        && data.NamedArguments.First(item => item.Key == "ImplementationType").Value.Value is INamedTypeSymbol,
                1 => true,
                _ => false
            };

            if (serviceExist
                && implementationExist
                && data.NamedArguments.Any(item => item.Key == "IsEnumerable")
                && data.NamedArguments.First(item => item.Key == "IsEnumerable").Value.Value is bool isEnumerable)
                return isEnumerable;
            return false;
        }
    }
}
