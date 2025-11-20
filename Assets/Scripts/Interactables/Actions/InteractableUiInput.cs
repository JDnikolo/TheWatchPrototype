using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Input/Enable UI Input")]
	public sealed class InteractableUiInput : Interactable
	{
		public override void Interact() => InputManager.Instance.ForceUIInput();
	}
}