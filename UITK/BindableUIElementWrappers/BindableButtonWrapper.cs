using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.BindableUIElementWrappers
{
	public class BindableButtonWrapper : BaseBindableButton
	{
		public BindableButtonWrapper(BindableButton button, IObjectProvider objectProvider)
			: base(button, objectProvider)
		{
		}
	}
}