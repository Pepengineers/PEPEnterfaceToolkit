using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.BindingContextObjectWrappers.CommandWrappers
{
    internal abstract class BaseCommandWrapper : IBaseCommand
    {
        private readonly IBaseCommand baseCommand;

        protected BaseCommandWrapper(IBaseCommand command)
        {
            baseCommand = command;
        }

        public event EventHandler<bool> CanExecuteChanged
        {
            add => baseCommand.CanExecuteChanged += value;
            remove => baseCommand.CanExecuteChanged -= value;
        }

        public bool CanExecute()
        {
            return baseCommand.CanExecute();
        }

        public void RaiseCanExecuteChanged()
        {
            baseCommand.RaiseCanExecuteChanged();
        }
    }
}