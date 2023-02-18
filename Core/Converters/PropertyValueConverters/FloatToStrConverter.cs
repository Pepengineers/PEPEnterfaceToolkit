using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Extensions;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Converters.PropertyValueConverters
{
	public class FloatToStrConverter : PropertyValueConverter<float, string>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string Convert(float value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override float ConvertBack(string value)
		{
			value.AsSpan().TryParse(out var result);
			return result;
		}
	}
}