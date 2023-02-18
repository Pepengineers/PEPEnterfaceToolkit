using System;
using System.Collections.Generic;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Runtime
{
	[DefaultExecutionOrder(1)]
	public abstract class MonoBehaviourView<TViewModel> : MonoBehaviour
		where TViewModel : IViewModel
	{
		private View view;

		public IViewModel ViewModel => view.ViewModel;

		private void Awake()
		{
			CreateView(GetViewModel());

			OnInit();
			BindElements();
		}

		protected virtual void OnEnable()
		{
			view.EnableBinding();
		}

		protected virtual void OnDisable()
		{
			view.DisableBinding();
		}

		protected virtual void OnDestroy()
		{
			view.Dispose();
		}

		protected abstract void OnInit();
		protected abstract IBindableElementsFactory GetBindableElementsFactory();
		protected abstract IEnumerable<IBindableUIElement> GetBindableUIElements();

		protected virtual TViewModel GetViewModel()
		{
			if (typeof(TViewModel).GetConstructor(Type.EmptyTypes) == null)
				throw new InvalidOperationException(
					$"Cannot create an instance of the type parameter {typeof(TViewModel)} because it does not have a parameterless constructor.");

			return Activator.CreateInstance<TViewModel>();
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

		private void CreateView(TViewModel vm)
		{
			view = new View(vm,
				GetObjectProvider(vm, GetValueConverters()),
				GetBindableElementsFactory());
		}

		private void BindElements()
		{
			foreach (var bindableUIElement in GetBindableUIElements())
				view.RegisterBindableElement(bindableUIElement);
		}
	}
}