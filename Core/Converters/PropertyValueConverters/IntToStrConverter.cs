using System.Runtime.CompilerServices;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Converters.PropertyValueConverters
{
	public class IntToStrConverter : PropertyValueConverter<int, string>
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string Convert(int value)
		{
			return value.ToString();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int ConvertBack(string value)
		{
			return int.Parse(value);
		}
	}
}