using UnityEngine;

namespace Interactables.Actions
{
	[AddComponentMenu("Interactables/Debug Toggle Interactable")]
	public sealed class DebugToggleInteractable : InteractableToggle
	{
		public override void OnInteractionChanged(bool playerEntered) =>
			Debug.Log($"Debug Interactable <{playerEntered}>");
	}
}