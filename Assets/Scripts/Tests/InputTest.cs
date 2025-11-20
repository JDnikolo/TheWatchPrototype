using UnityEngine;
using UnityEngine.InputSystem;

namespace Tests
{
	public sealed class InputTest : MonoBehaviour
	{
		[SerializeField] private InputActionAsset actionAsset;

		private void Start()
		{
			Debug.Log(actionAsset.bindingMask);
			actionAsset.bindingMask = new InputBinding {groups = "Keyboard&Mouse"};
			foreach (var controlScheme in actionAsset.controlSchemes) Debug.Log(controlScheme.name);
		}

		private void Update()
		{
			var action = actionAsset.FindActionMap("UI").FindAction("Navigate");
			Debug.Log(action.ReadValue<Vector2>());
		}
	}
}