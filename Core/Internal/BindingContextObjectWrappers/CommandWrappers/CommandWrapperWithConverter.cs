using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using PEPEngineers.PEPEnterfaceToolkit.Core.Internal.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.CommandWrappers
{
    internal class CommandWrapperWithConverter<TCommandValueType> : BaseCommandWrapper, ICommandWrapperWithParameter
    {
        private readonly ICommand<TCommandValueType> command;
        private readonly IParameterValueConverter<TCommandValueType> parameterConverter;
        private readonly Dictionary<int, TCommandValueType> parameters;

        public CommandWrapperWithConverter(ICommand<TCommandValueType> command,
            IParameterValueConverter<TCommandValueType> parameterConverter) : base(command)
        {
            this.command = command;
            this.parameterConverter = parameterConverter;
            parameters = new Dictionary<int, TCommandValueType>();
        }

        public void SetParameter(int elementId, ReadOnlyMemory<char> parameter)
        {
            parameters.Add(elementId, parameterConverter.Convert(parameter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int elementId)
        {
            command?.Execute(parameters[elementId]);
        }
    }
}