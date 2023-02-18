using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK
{
	public class BindableElementsFactory : IBindableElementsFactory
	{
		public virtual IBindableElement Create(IBindableUIElement bindableUiElement, IObjectProvider objectProvider)
		{
			return bindableUiElement switch
			{
				BindableLabel label => new BindableLabelWrapper(label, objectProvider),
				BindableTextField textField => new BindableTextFieldWrapper(textField, objectProvider),
				BindableButton button => new BindableButtonWrapper(button, objectProvider),

				_ => throw new NotImplementedException(
					$"Bindable visual element for {bindableUiElement.GetType()} is not implemented.")
			};
		}
	}
}