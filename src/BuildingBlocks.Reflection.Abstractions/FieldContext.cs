using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace BuildingBlocks.Reflection.Abstractions
{
    public abstract class FieldContext : IFieldContext
    {
        protected delegate TValue ValueGetter<TTarget, TValue>(ref TTarget target);

        protected delegate void ValueSetter<TTarget, TValue>(ref TTarget target, TValue value);

        protected abstract FrozenDictionary<Type, FrozenDictionary<string, Delegate>> Getters { get; }

        protected abstract FrozenDictionary<Type, FrozenDictionary<string, Delegate>> Setters { get; }

        public TValue GetValue<TTarget, TValue>(ref TTarget target, string name)
        {
            var targetType = typeof(TTarget);
            if (Getters.TryGetValue(targetType, out var getters)
                && getters.TryGetValue(name, out var row)
                && row is ValueGetter<TTarget, TValue> getter)
                return getter(ref target);

            ThrowKeyNotFound(targetType, name);
            return default;

            [DoesNotReturn]
            static void ThrowKeyNotFound(Type targetType, string name)
                => throw new KeyNotFoundException($"Cannot find generated field getter {name} in {targetType.Name}.");
        }

        public void SetValue<TTarget, TValue>(ref TTarget target, string name, TValue value)
        {
            var targetType = typeof(TTarget);
            if (Setters.TryGetValue(typeof(TTarget), out var setters)
                && setters.TryGetValue(name, out var row)
                && row is ValueSetter<TTarget, TValue> setter)
                setter(ref target, value);

            ThrowKeyNotFound(targetType, name);

            [DoesNotReturn]
            static void ThrowKeyNotFound(Type targetType, string name)
                => throw new KeyNotFoundException($"Cannot find generated field setter {name} in {targetType.Name}.");
        }
    }
}
