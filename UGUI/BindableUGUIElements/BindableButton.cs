using PEPEngineers.PEPEnterfaceToolkit.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PEPEngineers.PEPEnterfaceToolkit.UGUI.BindableUGUIElements
{
	[RequireComponent(typeof(Button))]
	public class BindableButton : MonoBehaviour, IBindableUIElement
	{
		[SerializeField] private Button button;
		[SerializeField] private string command;

		public string Command => command;

		public event UnityAction Click
		{
			add => button.onClick.AddListener(value);
			remove => button.onClick.RemoveListener(value);
		}
	}
}