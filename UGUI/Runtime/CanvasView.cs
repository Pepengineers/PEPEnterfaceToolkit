using System.Collections.Generic;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Runtime;
using UnityEngine;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.Runtime
{
	public abstract class CanvasView<TViewModel> : MonoBehaviourView<TViewModel>
		where TViewModel : IViewModel
	{
		public GameObject RootElement { get; private set; }

		protected override void OnInit()
		{
			RootElement = gameObject;
		}

		protected override IBindableElementsFactory GetBindableElementsFactory()
		{
			return new BindableElementsFactory();
		}

		protected override IEnumerable<IBindableUIElement> GetBindableUIElements()
		{
			return RootElement.GetComponentsInChildren<IBindableUIElement>(true);
		}
	}
}