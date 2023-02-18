namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
	public interface IProperty<TValueType>
	{
		TValueType Value { get; set; }
	}
}