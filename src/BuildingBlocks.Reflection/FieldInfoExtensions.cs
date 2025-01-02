using System.Runtime.CompilerServices;

namespace System.Reflection
{
    public static class FieldInfoExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe TValue GetValue<TTarget, TValue>(this FieldInfo fieldInfo, ref TTarget target)
        {
            var offset = Environment.Is64BitProcess ? 
                FieldDesc64.GetOffset(fieldInfo) : FieldDesc32.GetOffset(fieldInfo);

            if (fieldInfo.DeclaringType.IsValueType)
            {
                var targetPtr = (nint*)Unsafe.AsPointer(ref target);
                targetPtr += offset / 8;
                return Unsafe.AsRef<TValue>(targetPtr);
            }
            else
            {
                var targetPtr = *(nint**)Unsafe.AsPointer(ref target);
                targetPtr += 1 + offset / 8;
                return Unsafe.AsRef<TValue>(targetPtr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool TrySetValue<TTarget, TValue>(this FieldInfo fieldInfo, ref TTarget target, TValue value)
        {
            var offset = Environment.Is64BitProcess ?
                FieldDesc64.GetOffset(fieldInfo) : FieldDesc32.GetOffset(fieldInfo);

            if (fieldInfo.IsInitOnly)
                return false;

            if (fieldInfo.DeclaringType.IsValueType)
            {
                var targetPtr = (nint*)Unsafe.AsPointer(ref target);
                targetPtr += offset / 8;
                Unsafe.AsRef<TValue>(targetPtr) = value;
                return true;
            }
            else
            {
                var targetPtr = *(nint**)Unsafe.AsPointer(ref target);
                targetPtr += 1 + offset / 8;
                Unsafe.AsRef<TValue>(targetPtr) = value;
                return true;
            }
        }
    }
}
