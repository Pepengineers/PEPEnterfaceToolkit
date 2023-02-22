using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements;
using TMPro;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElementWrappers
{
	public class BindableInputFieldWrapper : BindablePropertyElement, IDisposable
	{
		private readonly TMP_InputField inputField;
		private readonly IProperty<string> textProperty;

		public BindableInputFieldWrapper(BindableInputField inputField, IObjectProvider objectProvider)
			: base(objectProvider)
		{
			this.inputField = inputField.InputField;
			textProperty = GetProperty<string>(inputField.BindingTextPath);
		}

		public override bool CanInitialize => textProperty != null;

		public override void Dispose()
		{
			inputField.onValueChanged.RemoveListener(OnInputFieldTextChanged);
		}

		public override void Initialize()
		{
			inputField.onValueChanged.AddListener(OnInputFieldTextChanged);
		}

		public override void UpdateValues()
		{
			var value = textProperty.Value;
			if (inputField.text != value) inputField.SetTextWithoutNotify(value);
		}

		private void OnInputFieldTextChanged(string text)
		{
			textProperty.Value = text;
		}
	}
}