﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.CommandWrappers;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.ObjectProviders
{
	internal class CommandProvider<TBindingContext> : ObjectProvider<TBindingContext>
	{
		private readonly HashSet<IParameterValueConverter> parameterConverters;

		internal CommandProvider(TBindingContext bindingContext, IEnumerable<IValueConverter> converters)
			: base(bindingContext)
		{
			parameterConverters = GetValueConverters<IParameterValueConverter>(converters);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TCommand GetCommand<TCommand>(string propertyName) where TCommand : IBaseCommand
		{
			if (string.IsNullOrWhiteSpace(propertyName)) return default;

			if (TryGetInstanceFromCache<TCommand>(propertyName, out var command)) return command;

			AssurePropertyExist(propertyName, out var propertyInfo);

			if (typeof(TCommand) != propertyInfo.PropertyType)
				throw new InvalidCastException(
					$"Can not cast the {propertyInfo.PropertyType} command to the {typeof(TCommand)} command.");

			return AddInstanceToCache<TCommand>(propertyName, propertyInfo.GetValue(BindingContext));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ICommandWrapper GetCommandWrapper(string propertyName, ReadOnlyMemory<char> parameterConverterName)
		{
			if (string.IsNullOrWhiteSpace(propertyName)) return default;

			if (TryGetInstanceFromCache<ICommandWrapper>(propertyName, out var commandWrapper)) return commandWrapper;

			AssurePropertyExist(propertyName, out var propertyInfo);

			var propertyType = propertyInfo.PropertyType;
			if (propertyType.GetInterface(nameof(IBaseCommand)) == null)
				throw new InvalidCastException(
					$"Can not cast the {propertyInfo.PropertyType} command to the {typeof(IBaseCommand)} command."); // TODO: Conditional?

			if (propertyType == typeof(ICommand) || propertyType.GetInterface(nameof(ICommand)) != null)
				return AddInstanceToCache<ICommandWrapper>(propertyName,
					new CommandWrapper((ICommand)propertyInfo.GetValue(BindingContext)));

			if (propertyType.IsGenericType == false)
				throw new InvalidCastException(
					$"Can not cast the {propertyInfo.PropertyType} command to the {typeof(ICommand<>)} command.");

			var commandValueType = propertyType.GenericTypeArguments[0];
			if (commandValueType == typeof(ReadOnlyMemory<char>))
				return AddInstanceToCache<ICommandWrapper>(propertyName,
					new CommandWrapperWithoutConverter(
						(ICommand<ReadOnlyMemory<char>>)propertyInfo.GetValue(BindingContext)));

			var args = new[]
			{
				propertyInfo.GetValue(BindingContext),
				GetParameterConverter(commandValueType, parameterConverterName.Span)
			};

			var genericCommandWrapperType = typeof(CommandWrapperWithConverter<>).MakeGenericType(commandValueType);

			return AddInstanceToCache<ICommandWrapper>(propertyName,
				Activator.CreateInstance(genericCommandWrapperType, args));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IParameterValueConverter GetParameterConverter(Type targetType, ReadOnlySpan<char> converterName)
		{
			var parameterConverter = converterName.IsEmpty
				? GetConverter(targetType)
				: GetConverter(targetType, converterName);

			if (parameterConverter == null)
				throw new NullReferenceException($"Parameter converter for {targetType} type is missing");

			return parameterConverter;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IParameterValueConverter GetConverter(Type targetType)
		{
			return parameterConverters.FirstOrDefault(converter => converter.TargetType == targetType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private IParameterValueConverter GetConverter(Type targetType, ReadOnlySpan<char> converterName)
		{
			foreach (var converter in parameterConverters)
				if (converter.TargetType == targetType &&
				    converterName.SequenceEqual(converter.Name))
					return converter;

			throw new NullReferenceException($"Parameter converter '{converterName.ToString()}' not found.");
		}
	}
}