using System;
using System.Collections.Generic;
using PEPEngineers.PEPEnterfaceToolkit.Core.Implementation;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Runtime
{
	[DefaultExecutionOrder(1)]
	public abstract class MonoBehaviourView<TBindingContext> : MonoBehaviour
		where TBindingContext : IViewModel
	{
		private View<TBindingContext> view;

		public TBindingContext BindingContext => view.BindingContext;

		private void Awake()
		{
			view = CreateView(GetBindingContext(), GetBindableElementsFactory());

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

		protected virtual TBindingContext GetBindingContext()
		{
			if (typeof(TBindingContext).GetConstructor(Type.EmptyTypes) == null)
				throw new InvalidOperationException(
					$"Cannot create an instance of the type parameter {typeof(TBindingContext)} because it does not have a parameterless constructor.");

			return Activator.CreateInstance<TBindingContext>();
		}

		protected virtual IValueConverter[] GetValueConverters()
		{
			return Array.Empty<IValueConverter>();
		}

		protected virtual IObjectProvider GetObjectProvider(TBindingContext bindingContext,
			IValueConverter[] converters)
		{
			return new BindingContextObjectProvider<TBindingContext>(bindingContext, converters);
		}

		private View<TBindingContext> CreateView(TBindingContext bindingContext,
			IBindableElementsFactory bindableElementsFactory)
		{
			return new View<TBindingContext>()
				.Configure(bindingContext, GetObjectProvider(bindingContext, GetValueConverters()),
					bindableElementsFactory);
		}

		private void BindElements()
		{
			foreach (var bindableUIElement in GetBindableUIElements())
				view.RegisterBindableElement(bindableUIElement, true);
		}
	}
}