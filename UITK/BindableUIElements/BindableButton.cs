using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements
{
	public class BindableButton : Button, IBindableUIElement
	{
		public bool Enabled
		{
			get => enabledSelf;
			set => SetEnabled(value);
		}

		public string Command { get; set; }

		public new class UxmlFactory : UxmlFactory<BindableButton, UxmlTraits>
		{
		}

		public new class UxmlTraits : Button.UxmlTraits
		{
			private readonly UxmlStringAttributeDescription commandAttribute = new()
				{ name = "command", defaultValue = "" };

			private readonly UxmlBoolAttributeDescription enabledAttribute = new()
				{ name = "enabled", defaultValue = true };

			public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
			{
				base.Init(visualElement, bag, context);

				var bindableButton = (BindableButton)visualElement;
				bindableButton.Enabled = enabledAttribute.GetValueFromBag(bag, context);
				bindableButton.Command = commandAttribute.GetValueFromBag(bag, context);
			}
		}
	}
}