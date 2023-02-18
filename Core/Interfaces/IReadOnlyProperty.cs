namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
    public interface IReadOnlyProperty<out TValueType>
    {
        TValueType Value { get; }
    }
}