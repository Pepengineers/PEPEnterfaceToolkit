using System;
using System.Collections.Generic;
using System.ComponentModel;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
    public class View<TBindingContext> : IDisposable where TBindingContext : class, INotifyPropertyChanged
    {
        private TBindingContext bindingContext;
        private IObjectProvider objectProvider;
        private IBindableElementsFactory bindableElementsFactory;

        private List<IDisposable> disposables;
        private Dictionary<string, HashSet<IBindablePropertyElement>> bindablePropertyElements;

        public TBindingContext BindingContext => bindingContext;

        public View<TBindingContext> Configure(TBindingContext bindingContext, IObjectProvider objectProvider,
            IBindableElementsFactory elementsFactory)
        {
            this.bindingContext = bindingContext;
            this.objectProvider = objectProvider;
            bindableElementsFactory = elementsFactory;

            disposables = new List<IDisposable>();
            bindablePropertyElements = new Dictionary<string, HashSet<IBindablePropertyElement>>();

            return this;
        }

        public void EnableBinding()
        {
            bindingContext.PropertyChanged += OnBindingContextPropertyChanged;
        }

        public void DisableBinding()
        {
            bindingContext.PropertyChanged -= OnBindingContextPropertyChanged;
        }

        public IBindableElement RegisterBindableElement(IBindableUIElement bindableUiElement, bool updateElementValues)
        {
            var bindableElement = bindableElementsFactory.Create(bindableUiElement, objectProvider);

            TryInitialize(bindableElement);
            TryRegisterPropertyElement(bindableElement, updateElementValues);

            return bindableElement;
        }

        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
        }

        private void TryInitialize(IBindableElement bindableElement)
        {
            var canInitialize = false;

            if (bindableElement is IInitializable { CanInitialize: true } initializable)
            {
                canInitialize = true;
                initializable.Initialize();
            }

            if (canInitialize && bindableElement is IDisposable disposable)
            {
                disposables.Add(disposable);
            }
        }

        private void TryRegisterPropertyElement(IBindableElement bindableElement, bool updateElementValues)
        {
            if (bindableElement is not IBindablePropertyElement bindablePropertyElement)
            {
                return;
            }

            foreach (var propertyName in bindablePropertyElement.BindableProperties)
            {
                RegisterBindableElement(propertyName, bindablePropertyElement);
            }

            if (updateElementValues && bindablePropertyElement.BindableProperties.Count > 0)
            {
                bindablePropertyElement.UpdateValues();
            }
        }

        private void RegisterBindableElement(string propertyName, IBindablePropertyElement bindablePropertyElement)
        {
            if (bindablePropertyElements.TryGetValue(propertyName, out var propertyElements))
            {
                propertyElements.Add(bindablePropertyElement);
            }
            else
            {
                bindablePropertyElements.Add(propertyName,
                    new HashSet<IBindablePropertyElement> { bindablePropertyElement });
            }
        }

        private void OnBindingContextPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (bindablePropertyElements.TryGetValue(e.PropertyName, out var propertyElements))
            {
                foreach (var propertyElement in propertyElements)
                {
                    propertyElement.UpdateValues();
                }
            }
        }
    }
}