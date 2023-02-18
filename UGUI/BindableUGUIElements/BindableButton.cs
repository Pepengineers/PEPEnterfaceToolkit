using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements
{
    [RequireComponent(typeof(Button))]
    public class BindableButton : MonoBehaviour, IBindableUIElement
    {
        [SerializeField] private Button _button;
        [SerializeField] private string _command;

        public string Command => _command;

        public event UnityAction Click
        {
            add => _button.onClick.AddListener(value);
            remove => _button.onClick.RemoveListener(value);
        }
    }
}