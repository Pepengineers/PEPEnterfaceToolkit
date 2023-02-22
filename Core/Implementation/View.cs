using System;
using System.Collections.Generic;
using System.ComponentModel;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
	public class View : IDisposable
	{
		private readonly IBindableElementsFactory bindableElementsFactory;
		private readonly Dictionary<string, HashSet<IBindablePropertyElement>> bindablePropertyElements;

		private readonly List<IDisposable> disposables;
		private readonly IObjectProvider objectProvider;

		public View(IViewModel context, IObjectProvider provider,
			IBindableElementsFactory elementsFactory)
		{
			disposables = new List<IDisposable>();
			bindablePropertyElements = new Dictionary<string, HashSet<IBindablePropertyElement>>();
			ViewModel = context;
			objectProvider = provider;
			bindableElementsFactory = elementsFactory;
		}

		public IViewModel ViewModel { get; }

		public void Dispose()
		{
			foreach (var disposable in disposables) disposable.Dispose();
		}

		public void EnableBinding()
		{
			ViewModel.PropertyChanged += OnBindingContextPropertyChanged;
		}

		public void DisableBinding()
		{
			ViewModel.PropertyChanged -= OnBindingContextPropertyChanged;
		}

		public void RegisterBindableElement(IBindableUIElement bindableUiElement)
		{
			var bindableElement = bindableElementsFactory.Create(bindableUiElement, objectProvider);

			Initialize(bindableElement);
			RegisterPropertyElement(bindableElement);
		}

		private void Initialize(IBindableElement bindableElement)
		{
			if (bindableElement.CanInitialize) bindableElement.Initialize();
			disposables.Add(bindableElement);
		}

		private void RegisterPropertyElement(IBindableElement bindableElement)
		{
			if (bindableElement is not IBindablePropertyElement bindablePropertyElement) return;

			foreach (var propertyName in bindablePropertyElement.BindableProperties)
				RegisterBindableElement(propertyName, bindablePropertyElement);

			if (bindablePropertyElement.BindableProperties.Count > 0)
				bindablePropertyElement.UpdateValues();
		}

		private void RegisterBindableElement(string propertyName, IBindablePropertyElement bindablePropertyElement)
		{
			if (bindablePropertyElements.TryGetValue(propertyName, out var propertyElements))
				propertyElements.Add(bindablePropertyElement);
			else
				bindablePropertyElements.Add(propertyName,
					new HashSet<IBindablePropertyElement> { bindablePropertyElement });
		}

		private void OnBindingContextPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (bindablePropertyElements.TryGetValue(e.PropertyName, out var propertyElements))
				foreach (var propertyElement in propertyElements)
					propertyElement.UpdateValues();
		}
	}
}