using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements
{
	public class BindableTextField : TextField, IBindableUIElement
	{
		public string BindingValuePath { get; set; }

		public new class UxmlFactory : UxmlFactory<BindableTextField, UxmlTraits>
		{
		}

		public new class UxmlTraits : TextField.UxmlTraits
		{
			private readonly UxmlStringAttributeDescription bindingValueAttribute = new()
				{ name = "binding-value-path", defaultValue = "" };

			public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
			{
				base.Init(visualElement, bag, context);
				((BindableTextField)visualElement).BindingValuePath =
					bindingValueAttribute.GetValueFromBag(bag, context);
			}
		}
	}
}