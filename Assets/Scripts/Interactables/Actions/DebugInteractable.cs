using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Debug Interactable")]
	public sealed class DebugInteractable : Interactable
	{
		public override void Interact() => Debug.Log("Debug Interact");
	}
}