using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using BenchmarkDotNet.Attributes;
using System.Reflection.Unsafe;

namespace BuildingBlocks.ReflectionBenchmark
{
    public class FieldGetValue
    {
        private TestClass _class;
        private TestStruct _struct;
        private FieldInfo _classFieldInfo;
        private FieldInfo _structFieldInfo;
        private TestGetter<TestClass, int> _expClassGetter;
        private TestGetter<TestClass, int> _emitClassGetter;
        private TestGetter<TestStruct, int> _expStructGetter;
        private TestGetter<TestStruct, int> _emitStructGetter;

        public FieldGetValue()
        {
            _class = new TestClass();
            _struct = new TestStruct();
            _classFieldInfo = typeof(TestClass).GetRuntimeFields().First(item => item.Name == "_index");
            _structFieldInfo = typeof(TestStruct).GetRuntimeFields().First(item => item.Name == "_index");
            _expClassGetter = GetExpGetter<TestClass, int>(_classFieldInfo);
            _emitClassGetter = GetEmitGetter<TestClass, int>(_classFieldInfo);
            _expStructGetter = GetExpGetter<TestStruct, int>(_structFieldInfo);
            _emitStructGetter = GetEmitGetter<TestStruct, int>(_structFieldInfo);
        }

        [Benchmark]
        public int ClassUnsafe()
            => _classFieldInfo.GetValueUnsafe<TestClass, int>(ref _class);

        [Benchmark]
        public int StructUnsafe()
            => _structFieldInfo.GetValueUnsafe<TestStruct, int>(ref _struct);

        [Benchmark]
        public int ClassGet()
            => (int)_classFieldInfo.GetValue(_class)!;

        [Benchmark]
        public int StructGet()
            => (int)_structFieldInfo.GetValue(_struct)!;

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
            => GetExpGetter<TestClass, int>(_classFieldInfo)(_class);
         
        [Benchmark]
        public int StructExpGet()
            => GetExpGetter<TestStruct, int>(_structFieldInfo)(_struct);

        [Benchmark]
        public int ClassEmitGet()
            => GetEmitGetter<TestClass, int>(_classFieldInfo)(_class);

        [Benchmark]
        public int StructEmitGet()
            => GetEmitGetter<TestStruct, int>(_structFieldInfo)(_struct);

        [Benchmark]
        public int Class()
            => _class.Index;

        [Benchmark]
        public int Struct()
            => _struct.Index;

        public static TestGetter<TTarget, TValue> GetExpGetter<TTarget, TValue>(FieldInfo fieldInfo)
        {
            var param = Expression.Parameter(typeof(TTarget));
            var fieldAccess = Expression.Field(param, fieldInfo);
            var lambda = Expression.Lambda<TestGetter<TTarget, TValue>>(fieldAccess, param);
            return lambda.Compile();
        }

        private static TestGetter<TTarget, TValue> GetEmitGetter<TTarget, TValue>(FieldInfo fieldInfo)
        {
            var valueType = typeof(TValue);
            var targetType = typeof(TTarget);
            var getterMethod = new DynamicMethod($"Get{targetType.Name}", valueType, [targetType]);

            var il = getterMethod.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, fieldInfo);
            il.Emit(OpCodes.Ret);

            return (TestGetter<TTarget, TValue>)getterMethod.CreateDelegate(typeof(TestGetter<TTarget, TValue>));
        }
    }
}