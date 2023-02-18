using System.Collections.Generic;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
    public interface IBindablePropertyElement : IBindableElement
    {
        IReadOnlyCollection<string> BindableProperties { get; }

        void UpdateValues();
    }
}