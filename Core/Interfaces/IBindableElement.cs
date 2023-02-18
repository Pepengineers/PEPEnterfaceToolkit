using System;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
	public interface IBindableElement : IDisposable
	{
		bool CanInitialize { get; }
		void Initialize();
	}
}