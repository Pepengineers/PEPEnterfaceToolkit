namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
	public interface IBindableElementsFactory
	{
		IBindableElement Create(IBindableUIElement bindableUiElement, IObjectProvider objectProvider);
	}
}