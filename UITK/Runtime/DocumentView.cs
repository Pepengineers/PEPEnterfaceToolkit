using System.Collections.Generic;
using System.Linq;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK.Runtime
{
	[RequireComponent(typeof(UIDocument))]
	public abstract class DocumentView<TViewModel> : MonoBehaviourView<TViewModel>
		where TViewModel : IViewModel
	{
		private UIDocument uiDocument;

		public VisualElement RootVisualElement => uiDocument == null ? null : uiDocument.rootVisualElement;

		protected override void OnInit()
		{
			uiDocument = GetComponent<UIDocument>();
		}

		protected override IBindableElementsFactory GetBindableElementsFactory()
		{
			return new BindableElementsFactory();
		}

		protected override IEnumerable<IBindableUIElement> GetBindableUIElements()
		{
			return RootVisualElement
				.Query<VisualElement>()
				.Where(element => element is IBindableUIElement)
				.Build()
				.Cast<IBindableUIElement>();
		}
	}
}