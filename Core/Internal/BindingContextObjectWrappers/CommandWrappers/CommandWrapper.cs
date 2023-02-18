using System.Runtime.CompilerServices;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.CommandWrappers
{
    internal class CommandWrapper : BaseCommandWrapper, ICommandWrapper
    {
        private readonly ICommand command;

        public CommandWrapper(ICommand command) : base(command)
        {
            this.command = command;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Execute(int elementId)
        {
            command?.Execute();
        }
    }
}