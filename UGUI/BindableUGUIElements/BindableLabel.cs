using TMPro;
using UnityEngine;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements
{
    [RequireComponent(typeof(TMP_Text))]
    public class BindableLabel : MonoBehaviour, IBindableUIElement
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private string _bindingTextPath;

        public TMP_Text Label => _label;
        public string BindingTextPath => _bindingTextPath;
    }
}