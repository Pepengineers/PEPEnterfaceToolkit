using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.CommandWrappers
{
	internal class CommandWrapperWithoutConverter : BaseCommandWrapper, ICommandWrapperWithParameter
	{
		private readonly ICommand<ReadOnlyMemory<char>> command;
		private readonly Dictionary<int, ReadOnlyMemory<char>> parameters;

		public CommandWrapperWithoutConverter(ICommand<ReadOnlyMemory<char>> command) : base(command)
		{
			this.command = command;
			parameters = new Dictionary<int, ReadOnlyMemory<char>>();
		}

		public void SetParameter(int elementId, ReadOnlyMemory<char> parameter)
		{
			parameters.Add(elementId, parameter);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Execute(int elementId)
		{
			command?.Execute(parameters[elementId]);
		}
	}
}