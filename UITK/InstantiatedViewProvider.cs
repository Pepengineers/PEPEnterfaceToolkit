using System;
using System.Collections.Generic;
using System.Linq;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine.UIElements;

namespace PEPEngineers.PEPEnterfaceToolkit.UITK
{
	public class InstantiatedViewProvider<TViewModel> where TViewModel : IViewModel
	{
		private readonly ICollection<IBindableUIElement> binds;
		private View view;

		public InstantiatedViewProvider(VisualElement userEntryAsset)
		{
			binds = userEntryAsset.Query<VisualElement>()
				.Where(element => element is IBindableUIElement)
				.Build()
				.Cast<IBindableUIElement>()
				.ToList();
		}

		protected virtual IValueConverter[] GetValueConverters()
		{
			return Array.Empty<IValueConverter>();
		}

		protected virtual IObjectProvider GetObjectProvider(TViewModel vm,
			IValueConverter[] converters)
		{
			return new BindingContextObjectProvider<TViewModel>(vm, converters);
		}

		protected virtual IBindableElementsFactory GetBindableElementsFactory()
		{
			return new BindableElementsFactory();
		}

		private void CreateViewForViewModel(TViewModel vm)
		{
			view = new View(vm, GetObjectProvider(vm, GetValueConverters()),
				GetBindableElementsFactory());
			
			foreach (var bindableUIElement in binds)
				view.RegisterBindableElement(bindableUIElement);
			
			view.EnableBinding();
		}

		public void Connect(TViewModel vm)
		{
			CreateViewForViewModel(vm);
		}
	}
}