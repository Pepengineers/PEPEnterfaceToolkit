using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements
{
	public class BindableInputField : MonoBehaviour, IBindableUIElement
	{
		[SerializeField] private TMP_InputField inputField;
		[SerializeField] private string bindingTextPath;

		public TMP_InputField InputField => inputField;
		public string BindingTextPath => bindingTextPath;
	}
}