#if UNITASK_SUPPORT

namespace Pepengineers.PEPEnterfaceToolkit.UniTask.Interfaces
{
    using UnityEngine.UIElements;
    
    public interface ITransitionPredicate
    {
        bool TransitionEnd(TransitionEndEvent e);
    }
}

#endif