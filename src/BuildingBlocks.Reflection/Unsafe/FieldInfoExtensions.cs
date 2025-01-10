using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.CompilerServices.Unsafe;

namespace System.Reflection.Unsafe
{
    public static class FieldInfoExtensions
    {
        [StructLayout(LayoutKind.Explicit)]
        internal readonly ref struct FieldDesc32
        {
            [FieldOffset(0)]
            private readonly nint m_pMTOfEnclosingClass;
            [FieldOffset(4)]
            private readonly uint m_dword1;
            [FieldOffset(8)]
            private readonly uint m_dword2;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public unsafe static int GetOffset(FieldInfo fieldInfo)
                => (int)(((FieldDesc32*)fieldInfo.FieldHandle.Value)->m_dword2 & 0x7FFFFFF);
        }

        [StructLayout(LayoutKind.Explicit)]
        internal readonly ref struct FieldDesc64
        {
            [FieldOffset(0)]
            private readonly nint m_pMTOfEnclosingClass;
            [FieldOffset(8)]
            private readonly uint m_dword1;
            [FieldOffset(12)]
            private readonly uint m_dword2;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public unsafe static int GetOffset(FieldInfo fieldInfo)
                => (int)(((FieldDesc64*)fieldInfo.FieldHandle.Value)->m_dword2 & 0x7FFFFFF);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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