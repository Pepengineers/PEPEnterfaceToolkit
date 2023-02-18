using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements
{
	public class BindableInputField : MonoBehaviour, IBindableUIElement
	{
		[SerializeField] private TMP_InputField _inputField;
		[SerializeField] private string _bindingTextPath;

		public TMP_InputField InputField => _inputField;
		public string BindingTextPath => _bindingTextPath;
	}
}