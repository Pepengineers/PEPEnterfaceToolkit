using System;
using System.Runtime.CompilerServices;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Converters.ParameterValueConverters
{
	public class ParameterToStrConverter : ParameterValueConverter<string>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string Convert(ReadOnlyMemory<char> parameter)
		{
			return parameter.ToString();
		}
	}
}