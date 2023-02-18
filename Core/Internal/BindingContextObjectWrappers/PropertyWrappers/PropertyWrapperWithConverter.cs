using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Extensions;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.PropertyWrappers
{
	internal class PropertyWrapperWithConverter<TObjectType, TValueType, TSourceType> : IProperty<TValueType>
	{
		private readonly Func<TObjectType, TSourceType> getPropertyDelegate;
		private readonly TObjectType obj;
		private readonly Action<TObjectType, TSourceType> setPropertyDelegate;
		private readonly IPropertyValueConverter<TSourceType, TValueType> valueConverter;

		public PropertyWrapperWithConverter(TObjectType obj, PropertyInfo propertyInfo,
			IPropertyValueConverter<TSourceType, TValueType> valueConverter)
		{
			this.obj = obj;
			this.valueConverter = valueConverter;
			getPropertyDelegate = propertyInfo.CreateGetValueDelegate<TObjectType, TSourceType>();
			setPropertyDelegate = propertyInfo.CreateSetValueDelegate<TObjectType, TSourceType>();
		}

		public TValueType Value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => valueConverter.Convert(getPropertyDelegate(obj));
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => setPropertyDelegate(obj, valueConverter.ConvertBack(value));
		}
	}
}