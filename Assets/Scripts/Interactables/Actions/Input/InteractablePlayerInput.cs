using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Actions.Input
{
	[AddComponentMenu("Interactables/Input/Enable Player Input")]
	public sealed class InteractablePlayerInput : Interactable
	{
		public override void Interact() => InputManager.Instance.ForcePlayerInput();
	}
}