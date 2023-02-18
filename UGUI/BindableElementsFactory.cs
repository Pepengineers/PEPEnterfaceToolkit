using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElementWrappers;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI
{
	public class BindableElementsFactory : IBindableElementsFactory
	{
		public virtual IBindableElement Create(IBindableUIElement bindableUiElement, IObjectProvider objectProvider)
		{
			return bindableUiElement switch
			{
				BindableLabel label => new BindableLabelWrapper(label, objectProvider),
				BindableInputField inputField => new BindableInputFieldWrapper(inputField, objectProvider),
				BindableButton button => new BindableButtonWrapper(button, objectProvider),

				_ => throw new NotImplementedException(
					$"Bindable visual element for {bindableUiElement.GetType()} is not implemented.")
			};
		}
	}
}