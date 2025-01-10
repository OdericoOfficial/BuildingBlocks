using System.Runtime.CompilerServices;

namespace System.Reflection.Unsafe
{
    public static class PropertyInfoExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool TryGetValueUnsafe<TTarget, TValue>(this PropertyInfo propertyInfo, ref TTarget target, out TValue? value)
        {
            value = default;
            if (!propertyInfo.CanRead)
                return false;

            var pointer = propertyInfo.GetGetMethod().MethodHandle.GetFunctionPointer();
            if (pointer == default)
                return false;

            if (propertyInfo.DeclaringType.IsValueType)
            {
                if (typeof(TTarget) == propertyInfo.DeclaringType)
                    value = GetValueUnsafeFromValueType<TTarget, TValue>(pointer, ref target);
            }
            else
                value = GetValueUnsafeFromReference<TTarget, TValue>(pointer, target);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool TrySetValueUnsafe<TTarget, TValue>(this PropertyInfo propertyInfo, ref TTarget target, TValue value)
        {
            if (!propertyInfo.CanWrite)
                return false;

            var pointer = propertyInfo.GetSetMethod().MethodHandle.GetFunctionPointer();
            if (pointer == default)
                return false;

            if (propertyInfo.DeclaringType.IsValueType)
            {
                if (typeof(TTarget) == propertyInfo.DeclaringType)
                    SetValueUnsafeFromValueType(pointer, ref target, value);
            }
            else
                SetValueUnsafeFromReference(pointer, target, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe TValue GetValueUnsafeFromReference<TTarget, TValue>(nint pointer, TTarget target)
            => ((delegate*<TTarget, TValue>)pointer)(target);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe TValue GetValueUnsafeFromValueType<TTarget, TValue>(nint pointer, ref TTarget target)
            => ((delegate*<ref TTarget, TValue>)pointer)(ref target);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void SetValueUnsafeFromReference<TTarget, TValue>(nint pointer, TTarget target, TValue value)
            => ((delegate*<TTarget, TValue, void>)pointer)(target, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void SetValueUnsafeFromValueType<TTarget, TValue>(nint pointer, ref TTarget target, TValue value)
            => ((delegate*<ref TTarget, TValue, void>)pointer)(ref target, value);
    }
}
