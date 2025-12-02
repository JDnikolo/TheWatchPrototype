using Attributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
	[CreateAssetMenu(fileName = "Control", menuName = "Input/Control")]
	public sealed class ControlObject : ScriptableObject
	{
		[SerializeField] private InputActionReference actionReference;
		[SerializeField] [DisableInInspector] private ControlSchemeObject controlScheme;
	}
}