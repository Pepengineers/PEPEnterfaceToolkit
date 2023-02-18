#if UNITASK_SUPPORT

namespace Pepengineers.PEPEnterfaceToolkit.UniTask.Interfaces
{
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using Pepengineers.PEPEnterfaceToolkit.Core.Interfaces;

    public interface IAsyncCommand : IBaseAsyncCommand, ICommand
    {
        UniTask ExecuteAsync(CancellationToken cancellationToken = default);
    }

    public interface IAsyncCommand<in T> : IBaseAsyncCommand, ICommand<T>
    {
        UniTask ExecuteAsync(T parameter, CancellationToken cancellationToken = default);
    }
}

#endif