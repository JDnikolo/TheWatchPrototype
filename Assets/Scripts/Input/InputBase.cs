using Localization.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	public abstract class InputBase : BaseObject
	{
		[SerializeField] [HideInInspector] private InputActionReference actionReference;
		[SerializeField] [HideInInspector] private TextObject displayText;
	}
}