using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements;
using TMPro;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElementWrappers
{
	public class BindableLabelWrapper : BindablePropertyElement
	{
		private readonly TMP_Text label;
		private readonly IReadOnlyProperty<string> textProperty;

		public BindableLabelWrapper(BindableLabel label, IObjectProvider objectProvider) : base(objectProvider)
		{
			this.label = label.Label;
			textProperty = GetReadOnlyProperty<string>(label.BindingTextPath);
		}

		public override void UpdateValues()
		{
			label.text = textProperty.Value;
		}

		public override void Dispose()
		{
			
		}

		public override bool CanInitialize => label != null;
		public override void Initialize()
		{
			
		}
	}
}