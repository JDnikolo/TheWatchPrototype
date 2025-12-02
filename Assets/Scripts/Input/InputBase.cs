using Localization.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	public abstract class InputBase : ScriptableObject
	{
		[SerializeField] [HideInInspector] private InputActionReference actionReference;
		[SerializeField] [HideInInspector] private TextObject displayText;
	}
}