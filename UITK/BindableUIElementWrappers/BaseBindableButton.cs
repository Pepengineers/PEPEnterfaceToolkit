using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
	public abstract class BaseBindableButton : BindableCommandElement, IDisposable
	{
		private readonly BindableButton button;
		private readonly int buttonId;
		private readonly ICommandWrapper commandWrapper;

		protected BaseBindableButton(BindableButton button, IObjectProvider objectProvider) : base(objectProvider)
		{
			this.button = button;
			buttonId = button.GetHashCode();
			commandWrapper = GetCommandWrapper(buttonId, button.Command);
		}

		public override void Dispose()
		{
			button.clicked -= OnButtonClicked;
			commandWrapper.CanExecuteChanged -= OnCommandCanExecuteChanged;
		}

		public override bool CanInitialize => commandWrapper != null;

		public override void Initialize()
		{
			button.clicked += OnButtonClicked;
			button.Enabled = commandWrapper.CanExecute();
			commandWrapper.CanExecuteChanged += OnCommandCanExecuteChanged;
		}

		private void OnButtonClicked()
		{
			commandWrapper.Execute(buttonId);
		}

		private void OnCommandCanExecuteChanged(object sender, bool canExecute)
		{
			button.Enabled = canExecute;
		}
	}
}