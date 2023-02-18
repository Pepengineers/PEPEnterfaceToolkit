using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements
{
	public class BindableLabel : Label, IBindableUIElement
	{
		public string BindingTextPath { get; set; }

		public new class UxmlFactory : UxmlFactory<BindableLabel, UxmlTraits>
		{
		}

		public new class UxmlTraits : Label.UxmlTraits
		{
			private readonly UxmlStringAttributeDescription bindingTextAttribute = new()
				{ name = "binding-text-path", defaultValue = "" };

			public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
			{
				base.Init(visualElement, bag, context);
				((BindableLabel)visualElement).BindingTextPath = bindingTextAttribute.GetValueFromBag(bag, context);
			}
		}
	}
}