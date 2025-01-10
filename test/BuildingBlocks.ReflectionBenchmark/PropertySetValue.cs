using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection;
using System.Reflection.Unsafe;
using BenchmarkDotNet.Attributes;

namespace BuildingBlocks.ReflectionBenchmark
{
    [MemoryDiagnoser]
    public class PropertySetValue
    {
        private TestClass _class;
        private TestStruct _struct;
        private PropertyInfo _classPropertyInfo;
        private PropertyInfo _structPropertyInfo;
        private TestSetter<TestClass, int> _expClassSetter;
        private TestSetter<TestClass, int> _emitClassSetter;
        private TestSetter<TestStruct, int> _expStructSetter;
        private TestSetter<TestStruct, int> _emitStructSetter;

        public PropertySetValue()
        {
            _class = new TestClass();
            _struct = new TestStruct();
            _classPropertyInfo = typeof(TestClass).GetProperties().First(item => item.Name == "Index");
            _structPropertyInfo = typeof(TestStruct).GetProperties().First(item => item.Name == "Index");
            _expClassSetter = GetExpSetter<TestClass, int>(_classPropertyInfo);
            _emitClassSetter = GetEmitSetter<TestClass, int>(_classPropertyInfo);
            _expStructSetter = GetExpSetter<TestStruct, int>(_structPropertyInfo);
            _emitStructSetter = GetEmitSetter<TestStruct, int>(_structPropertyInfo);
        }

        [Benchmark]
        public bool ClassUnsafe()
            => _classPropertyInfo.TrySetValueUnsafe(ref _class, 0);

        [Benchmark]
        public bool StructUnsafe()
            => _structPropertyInfo.TrySetValueUnsafe(ref _struct, 0);

        [Benchmark]
        public void ClassSet()
            => _classPropertyInfo.SetValue(_class, 0);

        [Benchmark]
        public void StructSet()
            => _structPropertyInfo.SetValue(_struct, 0);

        [Benchmark]
        public void ClassExpPreloadSet()
            => _expClassSetter(ref _class, 0);

        [Benchmark]
        public void StructExpPreloadSet()
            => _expStructSetter(ref _struct, 0);

        [Benchmark]
        public void ClassEmitPreloadSet()
            => _emitClassSetter(ref _class, 0);

        [Benchmark]
        public void StructEmitPreloadSet()
            => _emitStructSetter(ref _struct, 0);

        [Benchmark]
        public void ClassExpSet()
            => GetExpSetter<TestClass, int>(_classPropertyInfo)(ref _class, 0);

        [Benchmark]
        public void StructExpSet()
            => GetExpSetter<TestStruct, int>(_structPropertyInfo)(ref _struct, 0);

        [Benchmark]
        public void ClassEmitSet()
            => GetEmitSetter<TestClass, int>(_classPropertyInfo)(ref _class, 0);

        [Benchmark]
        public void StructEmitSet()
            => GetEmitSetter<TestStruct, int>(_structPropertyInfo)(ref _struct, 0);

        [Benchmark]
        public void Class()
            => _class.Index = 0;

        [Benchmark]
        public void Struct()
            => _struct.Index = 0;

        public static TestSetter<TTarget, TValue> GetExpSetter<TTarget, TValue>(PropertyInfo propertyInfo)
        {
            var targetExpression = Expression.Parameter(typeof(TTarget).MakeByRefType());
            var valueExpression = Expression.Parameter(typeof(TValue));
            var propertyExpression = Expression.Property(targetExpression, propertyInfo);
            var assignExpression = Expression.Assign(propertyExpression, valueExpression);
            var lambda = Expression.Lambda<TestSetter<TTarget, TValue>>(assignExpression, targetExpression, valueExpression);
            return lambda.Compile();
        }

        private static TestSetter<TTarget, TValue> GetEmitSetter<TTarget, TValue>(PropertyInfo propertyInfo)
        {
            var valueType = typeof(TValue);
            var targetType = typeof(TTarget);
            var dynamicMethod = new DynamicMethod($"Set{targetType.Name}", null, [targetType.MakeByRefType(), valueType]);

            var ilGenerator = dynamicMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Call, propertyInfo.GetSetMethod()!);
            ilGenerator.Emit(OpCodes.Ret);

            return (TestSetter<TTarget, TValue>)dynamicMethod.CreateDelegate(typeof(TestSetter<TTarget, TValue>));
        }

    }
}
