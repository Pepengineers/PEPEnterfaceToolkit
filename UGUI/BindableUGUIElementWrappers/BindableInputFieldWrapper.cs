using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements;
using TMPro;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElementWrappers
{
	public class BindableInputFieldWrapper : BindablePropertyElement, IInitializable, IDisposable
	{
		private readonly TMP_InputField _inputField;
		private readonly IProperty<string> _textProperty;

		public BindableInputFieldWrapper(BindableInputField inputField, IObjectProvider objectProvider)
			: base(objectProvider)
		{
			_inputField = inputField.InputField;
			_textProperty = GetProperty<string>(inputField.BindingTextPath);
		}

		public void Dispose()
		{
			_inputField.onValueChanged.RemoveListener(OnInputFieldTextChanged);
		}

		public bool CanInitialize => _textProperty != null;

		public void Initialize()
		{
			_inputField.onValueChanged.AddListener(OnInputFieldTextChanged);
		}

		public override void UpdateValues()
		{
			var value = _textProperty.Value;
			if (_inputField.text != value) _inputField.SetTextWithoutNotify(value);
		}

		private void OnInputFieldTextChanged(string text)
		{
			_textProperty.Value = text;
		}
	}
}