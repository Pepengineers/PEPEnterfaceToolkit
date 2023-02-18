using System;
using System.Runtime.CompilerServices;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.Structs
{
	internal ref struct LineSplitEnumerator
	{
		private int index;
		private int start;
		private ReadOnlySpan<char> str;
		private readonly char separator;
		private readonly bool trimLines;

		internal LineSplitEnumerator(ReadOnlySpan<char> str, char separator, bool trimLines)
		{
			this.str = str;
			index = 0;
			start = 0;
			this.separator = separator;
			this.trimLines = trimLines;

			Current = default;
		}

		public LineSplitData Current { get; private set; }

		public LineSplitEnumerator GetEnumerator()
		{
			return this;
		}

		public bool MoveNext()
		{
			var span = str;
			if (span.Length == 0) return false;

			var index = span.IndexOf(separator);
			if (index == -1)
			{
				str = ReadOnlySpan<char>.Empty;
				Current = CreateNewLine(this.index, start, span);

				return true;
			}

			Current = CreateNewLine(this.index, start, span.Slice(0, index));

			this.index++;
			start += index + 1;
			str = span.Slice(index + 1);

			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private LineSplitData CreateNewLine(int index, int start, ReadOnlySpan<char> data)
		{
			return trimLines
				? new LineSplitData(index, start, data).Trim()
				: new LineSplitData(index, start, data);
		}
	}
}