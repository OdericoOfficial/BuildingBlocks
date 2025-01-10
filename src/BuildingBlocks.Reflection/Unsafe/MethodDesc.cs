using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Unsafe
{
    [StructLayout(LayoutKind.Explicit)]
    internal readonly ref struct MethodDesc
    {
        [FieldOffset(0)]
        private readonly ushort m_wFlags3AndTokenRemainder;
        [FieldOffset(2)]
        private readonly byte m_chunkIndex;
        [FieldOffset(3)]
        private readonly byte m_bFlags4;
        [FieldOffset(4)]
        private readonly ushort m_wSlotNumber;
        [FieldOffset(6)]
        private readonly ushort m_wFlags;
        [FieldOffset(7)]
        private readonly nint m_codeData;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static nint GetFunctionPointer(MethodInfo methodInfo)
            => ((MethodDesc*)methodInfo.MethodHandle.Value)->m_codeData;
    }
}