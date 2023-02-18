using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Extensions;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.PropertyWrappers
{
    internal class ReadOnlyPropertyWrapper<TObjectType, TValueType> : IReadOnlyProperty<TValueType>
    {
        private readonly TObjectType obj;
        private readonly Func<TObjectType, TValueType> getPropertyDelegate;

        public ReadOnlyPropertyWrapper(TObjectType obj, PropertyInfo propertyInfo)
        {
            this.obj = obj;
            getPropertyDelegate = propertyInfo.CreateGetValueDelegate<TObjectType, TValueType>();
        }

        public TValueType Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => getPropertyDelegate(obj);
        }
    }
}