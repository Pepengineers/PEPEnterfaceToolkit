using System;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.Core.Internal.Interfaces
{
	internal interface ICommandWrapperWithParameter : ICommandWrapper
	{
		void SetParameter(int elementId, ReadOnlyMemory<char> parameter);
	}
}