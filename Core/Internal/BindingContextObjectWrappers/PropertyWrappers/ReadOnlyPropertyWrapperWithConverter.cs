using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Extensions;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.PropertyWrappers
{
	internal class ReadOnlyPropertyWrapperWithConverter<TObjectType, TValueType, TSourceType>
		: IReadOnlyProperty<TValueType>
	{
		private readonly Func<TObjectType, TSourceType> getPropertyDelegate;
		private readonly TObjectType obj;
		private readonly IPropertyValueConverter<TSourceType, TValueType> valueConverter;

		public ReadOnlyPropertyWrapperWithConverter(TObjectType obj, PropertyInfo propertyInfo,
			IPropertyValueConverter<TSourceType, TValueType> valueConverter)
		{
			this.obj = obj;
			this.valueConverter = valueConverter;
			getPropertyDelegate = propertyInfo.CreateGetValueDelegate<TObjectType, TSourceType>();
		}

		public TValueType Value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => valueConverter.Convert(getPropertyDelegate(obj));
		}
	}
}