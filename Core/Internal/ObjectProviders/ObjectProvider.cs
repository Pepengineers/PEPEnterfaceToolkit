using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.ObjectProviders
{
    internal abstract class ObjectProvider<TBindingContext>
    {
        private readonly TBindingContext bindingContext;
        private readonly Dictionary<(string, Type), object> cachedInstances;

        internal ObjectProvider(TBindingContext bindingContext)
        {
            this.bindingContext = bindingContext;
            cachedInstances = new Dictionary<(string, Type), object>();
        }

        protected TBindingContext BindingContext => bindingContext;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected HashSet<T> GetValueConverters<T>(IEnumerable<IValueConverter> converters) where T : IValueConverter
        {
            var result = new HashSet<T>();

            foreach (var converter in converters)
            {
                if (converter is T valueConverter)
                {
                    result.Add(valueConverter);
                }
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void AssurePropertyExist(string propertyName, out PropertyInfo propertyInfo)
        {
            propertyInfo = bindingContext.GetType().GetProperty(propertyName);
            if (propertyInfo == null)
            {
                throw new NullReferenceException($"Property '{propertyName}' not found.");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected T AddInstanceToCache<T>(string propertyName, object instance)
        {
            cachedInstances.Add((propertyName, typeof(T)), instance);
            return (T) instance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected bool TryGetInstanceFromCache<T>(string propertyName, out T instance)
        {
            if (cachedInstances.TryGetValue((propertyName, typeof(T)), out var cachedInstance))
            {
                instance = (T) cachedInstance;
                return true;
            }

            instance = default;
            return false;
        }
    }
}