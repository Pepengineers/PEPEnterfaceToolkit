using System;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.StringParsers;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Implementation
{
	public abstract class BindableCommandElement : IBindableElement
	{
		private readonly CommandStringParser commandStringParser;
		private readonly IObjectProvider objectProvider;

		protected BindableCommandElement(IObjectProvider objectProvider)
		{
			this.objectProvider = objectProvider;
			commandStringParser = new CommandStringParser();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected TCommand GetCommand<TCommand>(string propertyName) where TCommand : IBaseCommand
		{
			return objectProvider.GetCommand<TCommand>(propertyName);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected ICommandWrapper GetCommandWrapper(int elementId, string commandStringData)
		{
			var commandData = commandStringParser.GetCommandData(commandStringData.AsMemory());
			var commandWrapper = objectProvider.GetCommandWrapper(commandData.PropertyName.ToString(),
				commandData.ParameterConverterName);

			if (commandWrapper is ICommandWrapperWithParameter commandWrapperWithParameter)
				commandWrapperWithParameter.SetParameter(elementId, commandData.ParameterValue);

			return commandWrapper;
		}

		public abstract void Dispose();
		public abstract bool CanInitialize { get; }
		public abstract void Initialize();
	}
}