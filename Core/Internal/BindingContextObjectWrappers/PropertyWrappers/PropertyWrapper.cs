using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Extensions;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.PropertyWrappers
{
    internal class PropertyWrapper<TObjectType, TValueType> : IProperty<TValueType>
    {
        private readonly TObjectType obj;
        private readonly Func<TObjectType, TValueType> getPropertyDelegate;
        private readonly Action<TObjectType, TValueType> setPropertyDelegate;

        public PropertyWrapper(TObjectType obj, PropertyInfo propertyInfo)
        {
            this.obj = obj;
            getPropertyDelegate = propertyInfo.CreateGetValueDelegate<TObjectType, TValueType>();
            setPropertyDelegate = propertyInfo.CreateSetValueDelegate<TObjectType, TValueType>();
        }

        public TValueType Value
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => getPropertyDelegate(obj);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => setPropertyDelegate(obj, value);
        }
    }
}