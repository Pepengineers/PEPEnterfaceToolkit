using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
	public class BindableLabelWrapper : BindablePropertyElement
	{
		private readonly BindableLabel label;
		private readonly IReadOnlyProperty<string> textProperty;

		public BindableLabelWrapper(BindableLabel label, IObjectProvider objectProvider) : base(objectProvider)
		{
			this.label = label;
			textProperty = GetReadOnlyProperty<string>(label.BindingTextPath);
		}

		public override void UpdateValues()
		{
			label.text = textProperty.Value;
		}
	}
}