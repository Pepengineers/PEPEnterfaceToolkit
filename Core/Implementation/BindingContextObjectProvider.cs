using System;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.ObjectProviders;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
    public class BindingContextObjectProvider<TBindingContext> : IObjectProvider
    {
        private readonly CommandProvider<TBindingContext> commandProvider;
        private readonly PropertyProvider<TBindingContext> propertyProvider;

        public BindingContextObjectProvider(TBindingContext bindingContext, IValueConverter[] converters)
        {
            commandProvider = new CommandProvider<TBindingContext>(bindingContext, converters);
            propertyProvider = new PropertyProvider<TBindingContext>(bindingContext, converters);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TCommand GetCommand<TCommand>(string propertyName) where TCommand : IBaseCommand
        {
            return commandProvider.GetCommand<TCommand>(propertyName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ICommandWrapper GetCommandWrapper(string propertyName, ReadOnlyMemory<char> parameterConverterName)
        {
            return commandProvider.GetCommandWrapper(propertyName, parameterConverterName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IProperty<TValueType> GetProperty<TValueType>(string propertyName, ReadOnlyMemory<char> converterName)
        {
            return propertyProvider.GetProperty<TValueType>(propertyName, converterName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IReadOnlyProperty<TValueType> GetReadOnlyProperty<TValueType>(string propertyName,
            ReadOnlyMemory<char> converterName)
        {
            return propertyProvider.GetReadOnlyProperty<TValueType>(propertyName, converterName);
        }
    }
}