using BenchmarkDotNet.Attributes;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Reflection;
using System.Reflection.Unsafe;

namespace BuildingBlocks.ReflectionBenchmark
{
    [MemoryDiagnoser]
    public class FieldSetValue
    {
        private TestClass _class;
        private TestStruct _struct;
        private FieldInfo _classFieldInfo;
        private FieldInfo _structFieldInfo;
        private TestSetter<TestClass, int> _expClassSetter;
        private TestSetter<TestClass, int> _emitClassSetter;
        private TestSetter<TestStruct, int> _expStructSetter;
        private TestSetter<TestStruct, int> _emitStructSetter;

        public FieldSetValue()
        {
            _class = new TestClass();
            _struct = new TestStruct();
            _classFieldInfo = typeof(TestClass).GetRuntimeFields().First(item => item.Name == "_index");
            _structFieldInfo = typeof(TestStruct).GetRuntimeFields().First(item => item.Name == "_index");
            _expClassSetter = GetExpSetter<TestClass, int>(_classFieldInfo);
            _emitClassSetter = GetEmitSetter<TestClass, int>(_classFieldInfo);
            _expStructSetter = GetExpSetter<TestStruct, int>(_structFieldInfo);
            _emitStructSetter = GetEmitSetter<TestStruct, int>(_structFieldInfo);
        }

        [Benchmark]
        public bool ClassUnsafe()
            => _classFieldInfo.TrySetValueUnsafe(ref _class, 0);

        [Benchmark]
        public bool StructUnsafe()
            => _structFieldInfo.TrySetValueUnsafe(ref _struct, 0);

        [Benchmark]
        public void ClassSet()
            => _classFieldInfo.SetValue(_class, 0);

        [Benchmark]
        public void StructSet()
            => _structFieldInfo.SetValue(_struct, 0);

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
            => GetExpSetter<TestClass, int>(_classFieldInfo)(ref _class, 0);

        [Benchmark]
        public void StructExpSet()
            => GetExpSetter<TestStruct, int>(_structFieldInfo)(ref _struct, 0);

        [Benchmark]
        public void ClassEmitSet()
            => GetEmitSetter<TestClass, int>(_classFieldInfo)(ref _class, 0);

        [Benchmark]
        public void StructEmitSet()
            => GetEmitSetter<TestStruct, int>(_structFieldInfo)(ref _struct, 0);

        [Benchmark]
        public void Class()
            => _class.Index = 0;

        [Benchmark]
        public void Struct()
            => _struct.Index = 0;

        public static TestSetter<TTarget, TValue> GetExpSetter<TTarget, TValue>(FieldInfo fieldInfo)
        {
            var targetExpression = Expression.Parameter(typeof(TTarget).MakeByRefType());
            var valueExpression = Expression.Parameter(typeof(TValue));
            var fieldExpression = Expression.Field(targetExpression, fieldInfo);
            var assignExpression = Expression.Assign(fieldExpression, valueExpression);
            var lambda = Expression.Lambda<TestSetter<TTarget, TValue>>(assignExpression, targetExpression, valueExpression);
            return lambda.Compile();
        }

        private static TestSetter<TTarget, TValue> GetEmitSetter<TTarget, TValue>(FieldInfo fieldInfo)
        {
            var valueType = typeof(TValue);
            var targetType = typeof(TTarget);
            var dynamicMethod = new DynamicMethod($"Set{targetType.Name}", null, [targetType.MakeByRefType(), valueType]);

            var ilGenerator = dynamicMethod.GetILGenerator();

            ilGenerator.Emit(OpCodes.Ldarg_0);
            if (!targetType.IsValueType)
                ilGenerator.Emit(OpCodes.Ldind_Ref);
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Stfld, fieldInfo); 
            ilGenerator.Emit(OpCodes.Ret);

            return (TestSetter<TTarget, TValue>)dynamicMethod.CreateDelegate(typeof(TestSetter<TTarget, TValue>));
        }
    }
}
