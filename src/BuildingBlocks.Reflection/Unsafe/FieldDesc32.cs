using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Unsafe
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
}
