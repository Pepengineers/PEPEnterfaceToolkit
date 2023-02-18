namespace PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces
{
	public interface ICommandWrapper : IBaseCommand
	{
		void Execute(int elementId);
	}
}