using System;
using System.Collections.Generic;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.StringParsers;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
    public abstract class BindablePropertyElement : BindableCommandElement, IBindablePropertyElement
    {
        private readonly List<string> bindableProperties;
        private readonly IObjectProvider objectProvider;
        private readonly PropertyStringParser propertyStringParser;

        protected BindablePropertyElement(IObjectProvider objectProvider) : base(objectProvider)
        {
            this.objectProvider = objectProvider;
            bindableProperties = new List<string>();
            propertyStringParser = new PropertyStringParser();
        }

        public IReadOnlyCollection<string> BindableProperties => bindableProperties;

        public abstract void UpdateValues();

        protected IProperty<TValueType> GetProperty<TValueType>(string propertyStringData)
        {
            var bindingData = propertyStringParser.GetPropertyData(propertyStringData.AsMemory());
            var propertyName = bindingData.PropertyName.ToString();

            var property = objectProvider.GetProperty<TValueType>(propertyName, bindingData.ConverterName);
            if (property != null)
            {
                bindableProperties.Add(propertyName);
            }

            return property;
        }

        protected IReadOnlyProperty<TValueType> GetReadOnlyProperty<TValueType>(string bindingStringData)
        {
            var bindingData = propertyStringParser.GetPropertyData(bindingStringData.AsMemory());
            var propertyName = bindingData.PropertyName.ToString();

            var property = objectProvider.GetReadOnlyProperty<TValueType>(propertyName, bindingData.ConverterName);
            if (property != null)
            {
                bindableProperties.Add(propertyName);
            }

            return property;
        }
    }
}