using Managers;
using UnityEngine;
using Utilities;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Input/Enable Player Input")]
	public sealed class InteractablePlayerInput : Interactable
	{
		public override void Interact() => InputManager.Instance.ForcePlayerInput();
	}
}