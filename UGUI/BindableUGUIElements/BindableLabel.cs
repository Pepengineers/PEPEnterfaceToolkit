using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using TMPro;
using UnityEngine;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements
{
	[RequireComponent(typeof(TMP_Text))]
	public class BindableLabel : MonoBehaviour, IBindableUIElement
	{
		[SerializeField] private TMP_Text label;
		[SerializeField] private string bindingTextPath;

		public TMP_Text Label => label;
		public string BindingTextPath => bindingTextPath;
	}
}