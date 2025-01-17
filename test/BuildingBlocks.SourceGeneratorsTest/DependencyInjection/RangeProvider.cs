using System.Text;

namespace BuildingBlocks.SourceGeneratorsTest.DependencyInjection
{
    internal static class RangeProvider
    {
        private const string InterfaceNamespace = "DependencyInjectionTest.Abstractions";
        private const string ClassNamespace = "DependencyInjectionTest";
        private const string AttributeNamespace = "BuildingBlocks.DependencyInjection.Attributes";

        private static class Name
        {
            public static string GetInterfaceName(string prefix, string postfix, string target)
                => $"I{prefix}{target}Test{postfix}";

            public static string GetGenericInterfaceName(string prefix, string postfix, string target)
                => $"I{prefix}{target}Test{postfix}<T1, T2>";

            public static string GetBoundGenericInterfaceName(string prefix, string postfix, string target)
                => $"I{prefix}{target}Test{postfix}<string, int>";

            public static string GetUnboundGenericInterfaceName(string prefix, string postfix, string target)
                => $"I{prefix}{target}Test{postfix}<, >";

            public static string GetClassName(string prefix, string postfix, string target)
                => $"{prefix}{target}Test{postfix}";

            public static string GetGenericClassName(string prefix, string postfix, string target)
                => $"{prefix}{target}Test{postfix}<T1, T2>";

            public static string GetBoundGenericClassName(string prefix, string postfix, string target)
                => $"{prefix}{target}Test{postfix}<string, int>";

            public static string GetUnboundGenericClassName(string prefix, string postfix, string target)
                => $"{prefix}{target}Test{postfix}<, >";

            public static string GetEmptyName(string prefix, string postfix, string target)
                => $"Empty{prefix}{target}Test{postfix}";

            public static string GetGenericEmptyName(string prefix, string postfix, string target)
                => $"Empty{prefix}{target}Test{postfix}<T1, T2>";

            public static string GetBoundGenericEmptyName(string prefix, string postfix, string target)
                => $"Empty{prefix}{target}Test{postfix}<string, int>";
        
            public static string GetUnboundGenericEmptyName(string prefix, string postfix, string target)
                => $"Empty{prefix}{target}Test{postfix}<, >";
        }

        private static class Source
        {
            public static string GetInterfaceSource(string interfaceName)
                => $@"namespace {InterfaceNamespace}
{{
    public interface {interfaceName} {{ }}
}}";

            public static string GetClassSource(string className)
                => $@"namespace {ClassNamespace}
{{
    internal class {className} {{ }}
}}";

            public static string GetClassSource(string className, string interfaceName)
                => $@"using {InterfaceNamespace};

namespace {ClassNamespace}
{{
    internal class {className} : {interfaceName} {{ }}
}}";

            public static string GetClassSource(string className, Func<string> attributeExpression)
                => $@"using {AttributeNamespace};

namespace {ClassNamespace}
{{
    {attributeExpression()}
    internal class {className} {{ }}
}}";

            public static string GetClassSource(string className, string interfaceName, Func<string> attributeExpression)
                => $@"using {InterfaceNamespace};
using {AttributeNamespace};

namespace {ClassNamespace}
{{
    {attributeExpression()}
    internal class {className} : {interfaceName} {{ }}
}}";
        }

        private static class Assembly
        {
            public static class ZeroTypeArguments
            {
                public static string GetAttribute(string prefix, string serviceName)
                    => $"[assembly: {prefix}Assembly(typeof({serviceName}))]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string key)
                    => $"[assembly: {prefix}Assembly(typeof({serviceName}), Key = \"{key}\")]";

                public static string GetAttribute(string prefix, string serviceName, string implementationName)
                    => $"[assembly: {prefix}Assembly(typeof({serviceName}), ImplementationType = typeof({implementationName}))]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string implementationName, string key)
                    => $"[assembly: {prefix}Assembly(typeof({serviceName}), ImplementationType = typeof({implementationName}), Key = \"{key}\")]";

                public static string GetAttributeWithEnumerable(string prefix, string serviceName, string implementationName)
                    => $"[assembly: {prefix}Assembly(typeof({serviceName}), ImplementationType = typeof({implementationName}), IsEnumerable = true)]";

                public static string GetAttributeWithKeyedEnumerable(string prefix, string serviceName, string implementationName, string key)
                    => $"[assembly: {prefix}Assembly(typeof({serviceName}), ImplementationType = typeof({implementationName}), Key = \"{key}\", IsEnumerable = true)]";
            }

            public static class OneTypeArguments
            {
                public static string GetAttribute(string prefix, string serviceName)
                    => $"[assembly: {prefix}Assembly<{serviceName}>]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string key)
                    => $"[assembly: {prefix}Assembly<{serviceName}>(Key = \"{key}\")]";

                public static string GetAttribute(string prefix, string serviceName, string implementationName)
                    => $"[assembly: {prefix}Assembly<{serviceName}>(ImplementationType = typeof({implementationName}))]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string implementationName, string key)
                    => $"[assembly: {prefix}Assembly<{serviceName}>(ImplementationType = typeof({implementationName}), Key = \"{key}\")]";

                public static string GetAttributeWithEnumerable(string prefix, string serviceName, string implementationName)
                    => $"[assembly: {prefix}Assembly<{serviceName}>(ImplementationType = typeof({implementationName}), IsEnumerable = true)]";

                public static string GetAttributeWithKeyedEnumerable(string prefix, string serviceName, string implementationName, string key)
                    => $"[assembly: {prefix}Assembly<{serviceName}>(ImplementationType = typeof({implementationName}), Key = \"{key}\", IsEnumerable = true)]";
            }

            public static class TwoTypeArguments
            {
                public static string GetAttribute(string prefix, string serviceName, string implementationName)
                    => $"[assembly: {prefix}Assembly<{serviceName}, {implementationName}>]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string implementationName, string key)
                    => $"[assembly: {prefix}Assembly<{serviceName}, {implementationName}>(Key = \"{key}\")]";

                public static string GetAttributeWithEnumerable(string prefix, string serviceName, string implementationName)
                    => $"[assembly: {prefix}Assembly<{serviceName}, {implementationName}>(IsEnumerable = true)]";

                public static string GetAttributeWithKeyedEnumerable(string prefix, string serviceName, string implementationName, string key)
                    => $"[assembly: {prefix}Assembly<{serviceName}, {implementationName}>(Key = \"{key}\", IsEnumerable = true)]";
            }
        }

        private static class Class
        {
            public static class ZeroTypeArguments
            {
                public static string GetAttribute(string prefix)
                    => $"[{prefix}Class]";

                public static string GetAttributeWithKey(string prefix, string key)
                    => $"[{prefix}Class(Key = \"{key}\")]";

                public static string GetAttribute(string prefix, string serviceName)
                    => $"[{prefix}Class(ServiceType = typeof({serviceName}))]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string key)
                    => $"[{prefix}Class(ServiceType = typeof({serviceName}), Key = \"{key}\")]";

                public static string GetAttributeWithEnumerable(string prefix, string serviceName)
                    => $"[{prefix}Class(ServiceType = typeof({serviceName}), IsEnumerable = true)]";

                public static string GetAttributeWithKeyedEnumerable(string prefix, string serviceName, string key)
                    => $"[{prefix}Class(ServiceType = typeof({serviceName}), Key = \"{key}\", IsEnumerable = true)]";
            }

            public static class OneTypeArguments
            {
                public static string GetAttribute(string prefix, string serviceName)
                    => $"[{prefix}Class<{serviceName}>]";

                public static string GetAttributeWithKey(string prefix, string serviceName, string key)
                    => $"[{prefix}Class<{serviceName}>(Key = \"{key}\")]";

                public static string GetAttributeWithEnumerable(string prefix, string serviceName)
                    => $"[{prefix}Class<{serviceName}>(IsEnumerable = true)]";

                public static string GetAttributeWithKeyedEnumerable(string prefix, string serviceName, string key)
                    => $"[{prefix}Class<{serviceName}>(Key = \"{key}\", IsEnumerable = true)]";
            }
        }

        public static IEnumerable<string> GetSources()
        {
            var sources = new List<string>();   
            ReadOnlySpan<string> prefixs = ["Singleton", "Scoped", "Transient"];
            var globalRegister = new StringBuilder();
            globalRegister.AppendLine($@"using {InterfaceNamespace};
using {ClassNamespace};
using {AttributeNamespace};
");

            GetHostedSources(globalRegister, sources);

            for (int i = 0; i < prefixs.Length; i++)
            {;
                GetAssemblySources(prefixs[i], globalRegister, sources);
                GetClassSources(prefixs[i], sources);
            }

            sources.Add(globalRegister.ToString());
            
            return sources;
        }

        private static void GetAssemblySources(string prefix, StringBuilder globalRegister, List<string> sources)
        {
            GetEmptySources(prefix, globalRegister, sources);
            GetSources(prefix, globalRegister, sources);

            static void GetEmptySources(string prefix, StringBuilder globalRegister, List<string> sources)
            {
                GetZeros(prefix, globalRegister, sources);
                GetZeroKeyeds(prefix, globalRegister, sources);
                GetOnes(prefix, globalRegister, sources);
                GetOneKeyeds(prefix, globalRegister, sources);

                static void GetZeros(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    var emtpyName = Name.GetEmptyName(prefix, "Zero", "Assembly");
                    var emtpyGenericNameForBound = Name.GetGenericEmptyName(prefix, "ZeroBound", "Assembly");
                    var emtpyGenericNameForUnbound = Name.GetGenericEmptyName(prefix, "ZeroUnbound", "Assembly");
                    var emtpyBoundGenericName = Name.GetBoundGenericEmptyName(prefix, "ZeroBound", "Assembly");
                    var emptyUnboundGenericName = Name.GetUnboundGenericEmptyName(prefix, "ZeroUnbound", "Assembly");
                    globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttribute(prefix, emtpyName));
                    globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttribute(prefix, emtpyBoundGenericName));
                    globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttribute(prefix, emptyUnboundGenericName));
                    sources.Add(Source.GetClassSource(emtpyName));
                    sources.Add(Source.GetClassSource(emtpyGenericNameForBound));
                    sources.Add(Source.GetClassSource(emtpyGenericNameForUnbound));
                }

                static void GetZeroKeyeds(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    var emtpyName = Name.GetEmptyName(prefix, "ZeroKeyed", "Assembly");
                    var emtpyGenericNameForBound = Name.GetGenericEmptyName(prefix, "ZeroKeyedBound", "Assembly");
                    var emtpyGenericNameForUnbound = Name.GetGenericEmptyName(prefix, "ZeroKeyedUnbound", "Assembly");
                    var emtpyBoundGenericName = Name.GetBoundGenericEmptyName(prefix, "ZeroKeyedBound", "Assembly");
                    var emptyUnboundGenericName = Name.GetUnboundGenericEmptyName(prefix, "ZeroKeyedUnbound", "Assembly");
                    globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKey(prefix, emtpyName, "ZeroKeyed"));
                    globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKey(prefix, emtpyBoundGenericName, "ZeroKeyedBound"));
                    globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKey(prefix, emptyUnboundGenericName, "ZeroKeyedUnbound"));
                    sources.Add(Source.GetClassSource(emtpyName));
                    sources.Add(Source.GetClassSource(emtpyGenericNameForBound));
                    sources.Add(Source.GetClassSource(emtpyGenericNameForUnbound));
                }

                static void GetOnes(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    var emtpyName = Name.GetEmptyName(prefix, "One", "Assembly");
                    globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttribute(prefix, emtpyName));
                    sources.Add(Source.GetClassSource(emtpyName));
                }

                static void GetOneKeyeds(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    var emtpyName = Name.GetEmptyName(prefix, "OneKeyed", "Assembly");
                    globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithKey(prefix, emtpyName, "OneKeyed"));
                    sources.Add(Source.GetClassSource(emtpyName));
                }
            }
        
            static void GetSources(string prefix, StringBuilder globalRegister, List<string> sources)
            {
                GetZeroSources(prefix, globalRegister, sources);
                GetOneSources(prefix, globalRegister, sources);
                GetTwoSources(prefix, globalRegister, sources);

                static void GetZeroSources(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    GetZeros(prefix, globalRegister, sources);
                    GetZeroKeyeds(prefix, globalRegister, sources);
                    GetZeroEnumerables(prefix, globalRegister, sources);
                    GetZeroKeyedEnumerables(prefix, globalRegister, sources);

                    static void GetZeros(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "Zero", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "ZeroBound", "Assembly");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroUnbound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "ZeroBound", "Assembly");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroUnbound", "Assembly");

                        var className = Name.GetClassName(prefix, "Zero", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "ZeroBound", "Assembly");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroUnbound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "ZeroBound", "Assembly");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroUnbound", "Assembly");

                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttribute(prefix, interfaceName, className));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttribute(prefix, interfaceBoundGenericName, classBoundGenericName));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttribute(prefix, interfaceUnboundGenericName, classUnboundGenericName));

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound));
                    }

                    static void GetZeroKeyeds(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "ZeroKeyed", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "ZeroKeyedBound", "Assembly");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroKeyedUnbound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "ZeroKeyedBound", "Assembly");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroKeyedUnbound", "Assembly");

                        var className = Name.GetClassName(prefix, "ZeroKeyed", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "ZeroKeyedBound", "Assembly");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroKeyedUnbound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "ZeroKeyedBound", "Assembly");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroKeyedUnbound", "Assembly");

                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKey(prefix, interfaceName, className, "ZeroKeyed"));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKey(prefix, interfaceBoundGenericName, classBoundGenericName, "ZeroKeyedBound"));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKey(prefix, interfaceUnboundGenericName, classUnboundGenericName, "ZeroKeyedUnbound"));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound));
                    }

                    static void GetZeroEnumerables(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "ZeroEnumerable", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "ZeroEnumerableBound", "Assembly");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroEnumerableUnbound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "ZeroEnumerableBound", "Assembly");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroEnumerableUnbound", "Assembly");

                        var className = Name.GetClassName(prefix, "ZeroEnumerable", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "ZeroEnumerableBound", "Assembly");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroEnumerableUnbound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "ZeroEnumerableBound", "Assembly");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroEnumerableUnbound", "Assembly");

                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithEnumerable(prefix, interfaceName, className));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithEnumerable(prefix, interfaceBoundGenericName, classBoundGenericName));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithEnumerable(prefix, interfaceUnboundGenericName, classUnboundGenericName));

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound));
                    }

                    static void GetZeroKeyedEnumerables(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "ZeroKeyedEnumerable", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "ZeroKeyedEnumerableBound", "Assembly");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroKeyedEnumerableUnbound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "ZeroKeyedEnumerableBound", "Assembly");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroKeyedEnumerableUnbound", "Assembly");

                        var className = Name.GetClassName(prefix, "ZeroKeyedEnumerable", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "ZeroKeyedEnumerableBound", "Assembly");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroKeyedEnumerableUnbound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "ZeroKeyedEnumerableBound", "Assembly");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroKeyedEnumerableUnbound", "Assembly");

                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceName, className, "ZeroKeyedEnumerable"));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceBoundGenericName, classBoundGenericName, "ZeroKeyedBoundEnumerable"));
                        globalRegister.AppendLine(Assembly.ZeroTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceUnboundGenericName, classUnboundGenericName, "ZeroKeyedUnboundEnumerable"));

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound));
                    }
                }

                static void GetOneSources(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    GetOnes(prefix, globalRegister, sources);
                    GetOneKeyeds(prefix, globalRegister, sources);
                    GetOneEnumerables(prefix, globalRegister, sources);
                    GetOneKeyedEnumerables(prefix, globalRegister, sources);

                    static void GetOnes(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "One", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "OneBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "OneBound", "Assembly");

                        var className = Name.GetClassName(prefix, "One", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "OneBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "OneBound", "Assembly");

                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttribute(prefix, interfaceName, className));
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttribute(prefix, interfaceBoundGenericName, classBoundGenericName));

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }

                    static void GetOneKeyeds(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "OneKeyed", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "OneKeyedBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "OneKeyedBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "OneKeyed", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "OneKeyedBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "OneKeyedBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithKey(prefix, interfaceName, className, "OneKeyed"));
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithKey(prefix, interfaceBoundGenericName, classBoundGenericName, "OneKeyedBound"));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }

                    static void GetOneEnumerables(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "OneEnumerable", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "OneEnumerableBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "OneEnumerableBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "OneEnumerable", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "OneEnumerableBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "OneEnumerableBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithEnumerable(prefix, interfaceName, className));
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithEnumerable(prefix, interfaceBoundGenericName, classBoundGenericName));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }

                    static void GetOneKeyedEnumerables(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "OneKeyedEnumerable", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "OneKeyedEnumerableBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "OneKeyedEnumerableBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "OneKeyedEnumerable", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "OneKeyedEnumerableBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "OneKeyedEnumerableBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceName, className, "OneKeyedEnumerable"));
                        globalRegister.AppendLine(Assembly.OneTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceBoundGenericName, classBoundGenericName, "OneKeyedBoundEnumerable"));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }
                }

                static void GetTwoSources(string prefix, StringBuilder globalRegister, List<string> sources)
                {
                    GetTwos(prefix, globalRegister, sources);
                    GetTwoKeyeds(prefix, globalRegister, sources);
                    GetTwoEnumerables(prefix, globalRegister, sources);
                    GetTwoKeyedEnumerables(prefix, globalRegister, sources);

                    static void GetTwos(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "Two", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "TwoBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "TwoBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "Two", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "TwoBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "TwoBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttribute(prefix, interfaceName, className));
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttribute(prefix, interfaceBoundGenericName, classBoundGenericName));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }

                    static void GetTwoKeyeds(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "TwoKeyed", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "TwoKeyedBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "TwoKeyedBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "TwoKeyed", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "TwoKeyedBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "TwoKeyedBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttributeWithKey(prefix, interfaceName, className, "TwoKeyed"));
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttributeWithKey(prefix, interfaceBoundGenericName, classBoundGenericName, "TwoKeyedBound"));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }

                    static void GetTwoEnumerables(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "TwoEnumerable", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "TwoEnumerableBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "TwoEnumerableBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "TwoEnumerable", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "TwoEnumerableBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "TwoEnumerableBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttributeWithEnumerable(prefix, interfaceName, className));
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttributeWithEnumerable(prefix, interfaceBoundGenericName, classBoundGenericName));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }

                    static void GetTwoKeyedEnumerables(string prefix, StringBuilder globalRegister, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "TwoKeyedEnumerable", "Assembly");
                        var interfaceGenericNameForBound = Name.GetGenericInterfaceName(prefix, "TwoKeyedEnumerableBound", "Assembly");
                        var interfaceBoundGenericName = Name.GetBoundGenericInterfaceName(prefix, "TwoKeyedEnumerableBound", "Assembly");
                        
                        var className = Name.GetClassName(prefix, "TwoKeyedEnumerable", "Assembly");
                        var classGenericNameForBound = Name.GetGenericClassName(prefix, "TwoKeyedEnumerableBound", "Assembly");
                        var classBoundGenericName = Name.GetBoundGenericClassName(prefix, "TwoKeyedEnumerableBound", "Assembly");
                        
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceName, className, "TwoKeyedEnumerable"));
                        globalRegister.AppendLine(Assembly.TwoTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceBoundGenericName, classBoundGenericName, "TwoKeyedBoundEnumerable"));
                        
                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForBound));
                        sources.Add(Source.GetClassSource(className, interfaceName));
                        sources.Add(Source.GetClassSource(classGenericNameForBound, interfaceGenericNameForBound));
                    }
                }
            }
        }

        private static void GetClassSources(string prefix, List<string> sources)
        {
            GetEmptySource(prefix, sources);
            GetSources(prefix, sources);

            static void GetEmptySource(string prefix, List<string> sources)
            {
                GetZeros(prefix, sources);
                GetZeroKeyeds(prefix, sources);
                
                static void GetZeros(string prefix, List<string> sources)
                {
                    var emtpyName = Name.GetEmptyName(prefix, "Zero", "Class");
                    var emtpyGenericNameForUnbound = Name.GetGenericEmptyName(prefix, "ZeroUnbound", "Class");
                    var emptyUnboundGenericName = Name.GetUnboundGenericEmptyName(prefix, "ZeroUnbound", "Class");
                    sources.Add(Source.GetClassSource(emtpyName, () => Class.ZeroTypeArguments.GetAttribute(prefix)));
                    sources.Add(Source.GetClassSource(emtpyGenericNameForUnbound, () => Class.ZeroTypeArguments.GetAttribute(prefix)));
                }

                static void GetZeroKeyeds(string prefix, List<string> sources)
                {
                    var emtpyName = Name.GetEmptyName(prefix, "ZeroKeyed", "Class");
                    var emtpyGenericNameForUnbound = Name.GetGenericEmptyName(prefix, "ZeroKeyedUnbound", "Class");
                    var emptyUnboundGenericName = Name.GetUnboundGenericEmptyName(prefix, "ZeroKeyedUnbound", "Class");
                    sources.Add(Source.GetClassSource(emtpyName, () => Class.ZeroTypeArguments.GetAttributeWithKey(prefix, "ZeroKeyed")));
                    sources.Add(Source.GetClassSource(emtpyGenericNameForUnbound, () => Class.ZeroTypeArguments.GetAttributeWithKey(prefix, "ZeroKeyedUnbound")));
                }
            }

            static void GetSources(string prefix, List<string> sources)
            {
                GetZeroSources(prefix, sources);
                GetOneSources(prefix, sources);

                static void GetZeroSources(string prefix, List<string> sources)
                {
                    GetZeros(prefix, sources);
                    GetZeroKeyeds(prefix, sources);
                    GetZeroEnumerables(prefix, sources);
                    GetZeroKeyedEnumerables(prefix, sources);

                    static void GetZeros(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "Zero", "Class");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroUnbound", "Class");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroUnbound", "Class");

                        var className = Name.GetClassName(prefix, "Zero", "Class");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroUnbound", "Class");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroUnbound", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.ZeroTypeArguments.GetAttribute(prefix, interfaceName)));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound, () => Class.ZeroTypeArguments.GetAttribute(prefix, interfaceUnboundGenericName)));
                    }

                    static void GetZeroKeyeds(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "ZeroKeyed", "Class");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroKeyedUnbound", "Class");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroKeyedUnbound", "Class");

                        var className = Name.GetClassName(prefix, "ZeroKeyed", "Class");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroKeyedUnbound", "Class");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroKeyedUnbound", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.ZeroTypeArguments.GetAttributeWithKey(prefix, interfaceName, "ZeroKeyed")));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound, () => Class.ZeroTypeArguments.GetAttributeWithKey(prefix, interfaceUnboundGenericName, "ZeroKeyedUnbound")));
                    }

                    static void GetZeroEnumerables(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "ZeroEnumerable", "Class");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroEnumerableUnbound", "Class");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroEnumerableUnbound", "Class");

                        var className = Name.GetClassName(prefix, "ZeroEnumerable", "Class");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroEnumerableUnbound", "Class");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroEnumerableUnbound", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.ZeroTypeArguments.GetAttributeWithEnumerable(prefix, interfaceName)));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound, () => Class.ZeroTypeArguments.GetAttributeWithEnumerable(prefix, interfaceUnboundGenericName)));
                    }

                    static void GetZeroKeyedEnumerables(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "ZeroKeyedEnumerable", "Class");
                        var interfaceGenericNameForUnbound = Name.GetGenericInterfaceName(prefix, "ZeroKeyedEnumerableUnbound", "Class");
                        var interfaceUnboundGenericName = Name.GetUnboundGenericInterfaceName(prefix, "ZeroKeyedEnumerableUnbound", "Class");

                        var className = Name.GetClassName(prefix, "ZeroKeyedEnumerable", "Class");
                        var classGenericNameForUnbound = Name.GetGenericClassName(prefix, "ZeroKeyedEnumerableUnbound", "Class");
                        var classUnboundGenericName = Name.GetUnboundGenericClassName(prefix, "ZeroKeyedEnumerableUnbound", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetInterfaceSource(interfaceGenericNameForUnbound));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.ZeroTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceName, "ZeroKeyedEnumerable")));
                        sources.Add(Source.GetClassSource(classGenericNameForUnbound, interfaceGenericNameForUnbound, () => Class.ZeroTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceUnboundGenericName, "ZeroKeyedEnumerableUnbound")));
                    }
                }

                static void GetOneSources(string prefix, List<string> sources)
                {
                    GetOnes(prefix, sources);
                    GetOneKeyeds(prefix, sources);
                    GetOneEnumerables(prefix, sources);
                    GetOneKeyedEnumerables(prefix, sources);

                    static void GetOnes(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "One", "Class");
                        var className = Name.GetClassName(prefix, "One", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.OneTypeArguments.GetAttribute(prefix, interfaceName)));
                    }

                    static void GetOneKeyeds(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "OneKeyed", "Class");
                        var className = Name.GetClassName(prefix, "OneKeyed", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.OneTypeArguments.GetAttributeWithKey(prefix, interfaceName, "OneKeyed")));
                    }

                    static void GetOneEnumerables(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "OneEnumerable", "Class");
                        var className = Name.GetClassName(prefix, "OneEnumerable", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName)); 
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.OneTypeArguments.GetAttributeWithEnumerable(prefix, interfaceName)));
                    }

                    static void GetOneKeyedEnumerables(string prefix, List<string> sources)
                    {
                        var interfaceName = Name.GetInterfaceName(prefix, "OneKeyedEnumerable", "Class");
                        var className = Name.GetClassName(prefix, "OneKeyedEnumerable", "Class");

                        sources.Add(Source.GetInterfaceSource(interfaceName));
                        sources.Add(Source.GetClassSource(className, interfaceName, () => Class.OneTypeArguments.GetAttributeWithKeyedEnumerable(prefix, interfaceName, "OneKeyedEnumerable")));
                    }
                }
            }
        }

        private static void GetHostedSources(StringBuilder globalRegister, List<string> sources)
        {
            var classSource = $@"using {AttributeNamespace};
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace {ClassNamespace}
{{
    [HostedService]
    internal class TestClassHostedService : IHostedService
    {{
        public Task StartAsync(CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task StopAsync(CancellationToken cancellationToken)
            => throw new NotImplementedException();
    }}
}}";
            var assemblySource = $@"using {AttributeNamespace};
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace {ClassNamespace}
{{
    internal class TestAssemblyHostedService : IHostedService
    {{
        public Task StartAsync(CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task StopAsync(CancellationToken cancellationToken)
            => throw new NotImplementedException();
    }}
}}";
            sources.Add(classSource);
            sources.Add(assemblySource);
            globalRegister.AppendLine("[assembly: HostedService<TestAssemblyHostedService>]");
        }
    }
}