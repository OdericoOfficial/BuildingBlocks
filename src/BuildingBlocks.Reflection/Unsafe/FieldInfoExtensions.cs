using static System.Runtime.CompilerServices.Unsafe;

namespace System.Reflection.Unsafe
{
    public static class FieldInfoExtensions
    {
        public static unsafe TValue GetValueUnsafe<TTarget, TValue>(this FieldInfo fieldInfo, ref TTarget target)
        {
            var offset = Environment.Is64BitProcess ?
                FieldDesc64.GetOffset(fieldInfo) : FieldDesc32.GetOffset(fieldInfo);

            if (fieldInfo.DeclaringType.IsValueType)
            {
                var targetPtr = (nint*)AsPointer(ref target);
                targetPtr += offset / 8;
                return AsRef<TValue>(targetPtr);
            }
            else
            {
                var targetPtr = *(nint**)AsPointer(ref target);
                targetPtr += 1 + offset / 8;
                return AsRef<TValue>(targetPtr);
            }
        }

        public static unsafe bool TrySetValueUnsafe<TTarget, TValue>(this FieldInfo fieldInfo, ref TTarget target, TValue value)
        {
            var offset = Environment.Is64BitProcess ?
                FieldDesc64.GetOffset(fieldInfo) : FieldDesc32.GetOffset(fieldInfo);

            if (fieldInfo.IsInitOnly)
                return false;

            if (fieldInfo.DeclaringType.IsValueType)
            {
                var targetPtr = (nint*)AsPointer(ref target);
                targetPtr += offset / 8;
                AsRef<TValue>(targetPtr) = value;
                return true;
            }
            else
            {
                var targetPtr = *(nint**)AsPointer(ref target);
                targetPtr += 1 + offset / 8;
                AsRef<TValue>(targetPtr) = value;
                return true;
            }
        }
    }
}