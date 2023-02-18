using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElementWrappers
{
	public class BindableButtonWrapper : BindableCommandElement, IInitializable, IDisposable
	{
		private readonly BindableButton _button;
		private readonly int _buttonId;
		private readonly ICommandWrapper _commandWrapper;

		public BindableButtonWrapper(BindableButton button, IObjectProvider objectProvider) : base(objectProvider)
		{
			_button = button;
			_buttonId = button.GetInstanceID();
			_commandWrapper = GetCommandWrapper(_buttonId, button.Command);
		}

		public void Dispose()
		{
			_button.Click -= OnButtonClicked;
		}

		public bool CanInitialize => _commandWrapper != null;

		public void Initialize()
		{
			_button.Click += OnButtonClicked;
		}

		private void OnButtonClicked()
		{
			_commandWrapper.Execute(_buttonId);
		}
	}
}