using System;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.Structs;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Extensions
{
    public static class ReadOnlyMemoryExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEmptyOrWhiteSpace(this ReadOnlyMemory<char> memory)
        {
            return memory.Span.IsEmptyOrWhiteSpace();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static LineSplitEnumerator Split(this ReadOnlyMemory<char> memory, char separator, bool trim = false)
        {
            return new LineSplitEnumerator(memory.Span, separator, trim);
        }
    }
}