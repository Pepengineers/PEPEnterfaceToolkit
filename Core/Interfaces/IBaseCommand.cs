using System;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
    public interface IBaseCommand
    {
        event EventHandler<bool> CanExecuteChanged;

        bool CanExecute();
        void RaiseCanExecuteChanged();
    }
}