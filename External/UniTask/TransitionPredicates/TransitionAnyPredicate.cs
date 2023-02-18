#if UNITASK_SUPPORT

namespace Pepengineers.PEPEnterfaceToolkit.UniTask.TransitionPredicates
{
    using Interfaces;
    using UnityEngine.UIElements;

    public readonly struct TransitionAnyPredicate : ITransitionPredicate
    {
        public bool TransitionEnd(TransitionEndEvent e) => true;
    }
}

#endif