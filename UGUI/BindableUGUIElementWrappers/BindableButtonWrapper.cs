using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElementWrappers
{
	public class BindableButtonWrapper : BindableCommandElement, IDisposable
	{
		private readonly BindableButton button;
		private readonly int buttonId;
		private readonly ICommandWrapper commandWrapper;

		public BindableButtonWrapper(BindableButton button, IObjectProvider objectProvider) : base(objectProvider)
		{
			this.button = button;
			buttonId = button.GetInstanceID();
			commandWrapper = GetCommandWrapper(buttonId, button.Command);
		}

		public override void Dispose()
		{
			button.Click -= OnButtonClicked;
		}

		public override bool CanInitialize => commandWrapper != null;

		public override void Initialize()
		{
			button.Click += OnButtonClicked;
		}

		private void OnButtonClicked()
		{
			commandWrapper.Execute(buttonId);
		}
	}
}