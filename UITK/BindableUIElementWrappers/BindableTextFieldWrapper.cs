using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
	// TODO: Reset value on leave.
	public class BindableTextFieldWrapper : BindablePropertyElement, IInitializable, IDisposable
	{
		private readonly BindableTextField textField;
		private readonly IProperty<string> valueProperty;

		public BindableTextFieldWrapper(BindableTextField textField, IObjectProvider objectProvider)
			: base(objectProvider)
		{
			this.textField = textField;
			valueProperty = GetProperty<string>(textField.BindingValuePath);
		}

		public void Dispose()
		{
			textField.UnregisterValueChangedCallback(OnTextFieldValueChanged);
		}

		public bool CanInitialize => valueProperty != null;

		public void Initialize()
		{
			textField.RegisterValueChangedCallback(OnTextFieldValueChanged);
		}

		public override void UpdateValues()
		{
			var value = valueProperty.Value;
			if (textField.value != value) textField.SetValueWithoutNotify(value);
		}

		private void OnTextFieldValueChanged(ChangeEvent<string> e)
		{
			valueProperty.Value = e.newValue;
		}
	}
}