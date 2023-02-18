using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements
{
	public class BindableScrollView : ScrollView, IBindableUIElement
	{
		public string BindingItemsSourcePath { get; set; }

		public new class UxmlFactory : UxmlFactory<BindableScrollView, UxmlTraits>
		{
		}

		public new class UxmlTraits : ScrollView.UxmlTraits
		{
			private readonly UxmlStringAttributeDescription bindingItemsSourceAttribute = new()
				{ name = "binding-items-source-path", defaultValue = "" };

			public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
			{
				base.Init(visualElement, bag, context);
				((BindableScrollView)visualElement).BindingItemsSourcePath =
					bindingItemsSourceAttribute.GetValueFromBag(bag, context);
			}
		}
	}
}