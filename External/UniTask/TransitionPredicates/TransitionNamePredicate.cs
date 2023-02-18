#if UNITASK_SUPPORT

namespace Pepengineers.PEPEnterfaceToolkit.UniTask.TransitionPredicates
{
    using Interfaces;
    using UnityEngine.UIElements;

    public readonly struct TransitionNamePredicate : ITransitionPredicate
    {
        private readonly StylePropertyName _stylePropertyName;

        public TransitionNamePredicate(StylePropertyName stylePropertyName)
        {
            _stylePropertyName = stylePropertyName;
        }

        public bool TransitionEnd(TransitionEndEvent e) => e.AffectsProperty(_stylePropertyName);
    }
}

#endif