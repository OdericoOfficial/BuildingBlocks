using BenchmarkDotNet.Attributes;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection;
using System.Reflection.Unsafe;

namespace BuildingBlocks.ReflectionBenchmark
{
    [MemoryDiagnoser]
    internal class PropertyGetValue
    {
        private TestClass _class;
        private TestStruct _struct;
        private PropertyInfo _classPropertyInfo;
        private PropertyInfo _structPropertyInfo;
        private TestGetter<TestClass, int> _expClassGetter;
        private TestGetter<TestClass, int> _emitClassGetter;
        private TestGetter<TestStruct, int> _expStructGetter;
        private TestGetter<TestStruct, int> _emitStructGetter;

        public PropertyGetValue()
        {
            _class = new TestClass();
            _struct = new TestStruct();
            _classPropertyInfo = typeof(TestClass).GetProperties().First(item => item.Name == "Index");
            _structPropertyInfo = typeof(TestStruct).GetProperties().First(item => item.Name == "Index");
            _expClassGetter = GetExpGetter<TestClass, int>(_classPropertyInfo);
            _emitClassGetter = GetEmitGetter<TestClass, int>(_classPropertyInfo);
            _expStructGetter = GetExpGetter<TestStruct, int>(_structPropertyInfo);
            _emitStructGetter = GetEmitGetter<TestStruct, int>(_structPropertyInfo);
        }

        [Benchmark]
        public bool ClassUnsafe()
            => _classPropertyInfo.TryGetValueUnsafe<TestClass, int>(ref _class, out var value);

        [Benchmark]
        public bool StructUnsafe()
            => _structPropertyInfo.TryGetValueUnsafe<TestStruct, int>(ref _struct, out var value);

        [Benchmark]
        public int ClassGet()
            => (int)_classPropertyInfo.GetValue(_class)!;

        [Benchmark]
        public int StructGet()
            => (int)_structPropertyInfo.GetValue(_struct)!;

        [Benchmark]
        public int ClassExpPreloadGet()
            => _expClassGetter(_class);

        [Benchmark]
        public int StructExpPreloadGet()
            => _expStructGetter(_struct);

        [Benchmark]
        public int ClassEmitPreloadGet()
            => _emitClassGetter(_class);

        [Benchmark]
        public int StructEmitPreloadGet()
            => _emitStructGetter(_struct);

        [Benchmark]
        public int ClassExpGet()
            => GetExpGetter<TestClass, int>(_classPropertyInfo)(_class);

        [Benchmark]
        public int StructExpGet()
            => GetExpGetter<TestStruct, int>(_structPropertyInfo)(_struct);

        [Benchmark]
        public int ClassEmitGet()
            => GetEmitGetter<TestClass, int>(_classPropertyInfo)(_class);

        [Benchmark]
        public int StructEmitGet()
            => GetEmitGetter<TestStruct, int>(_structPropertyInfo)(_struct);

        [Benchmark]
        public int Class()
            => _class.Index;

        [Benchmark]
        public int Struct()
            => _struct.Index;

        public static TestGetter<TTarget, TValue> GetExpGetter<TTarget, TValue>(PropertyInfo propertyInfo)
        {
            var param = Expression.Parameter(typeof(TTarget));
            var propertyAccess = Expression.Property(param, propertyInfo);
            var lambda = Expression.Lambda<TestGetter<TTarget, TValue>>(propertyAccess, param);
            return lambda.Compile();
        }

        private static TestGetter<TTarget, TValue> GetEmitGetter<TTarget, TValue>(PropertyInfo propertyInfo)
        {
            var valueType = typeof(TValue);
            var targetType = typeof(TTarget);
            var getterMethod = new DynamicMethod($"Get{targetType.Name}", valueType, [targetType]);

            var il = getterMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, propertyInfo.GetGetMethod()!);
            il.Emit(OpCodes.Ret);

            return (TestGetter<TTarget, TValue>)getterMethod.CreateDelegate(typeof(TestGetter<TTarget, TValue>));
        }
    }
}
