using Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	[CreateAssetMenu(fileName = "Control", menuName = "Input/Control")]
	public sealed class ControlObject : BaseObject
	{
		[SerializeField] private InputActionReference actionReference;
		[SerializeField] [DisableInInspector] private ControlSchemeObject controlScheme;
	}
}