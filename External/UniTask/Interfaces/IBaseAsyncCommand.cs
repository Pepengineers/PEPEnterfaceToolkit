#if UNITASK_SUPPORT
namespace Pepengineers.PEPEnterfaceToolkit.UniTask.Interfaces
{
    public interface IBaseAsyncCommand
    {
        bool IsRunning { get; }
        bool DisableOnExecution { get; set; }

        void Cancel();
    }
}

#endif