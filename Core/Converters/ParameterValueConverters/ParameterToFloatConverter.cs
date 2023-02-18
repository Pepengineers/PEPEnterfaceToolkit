using System;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Extensions;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Converters.ParameterValueConverters
{
	public class ParameterToFloatConverter : ParameterValueConverter<float>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override float Convert(ReadOnlyMemory<char> parameter)
		{
			parameter.Span.TryParse(out var result);
			return result;
		}
	}
}