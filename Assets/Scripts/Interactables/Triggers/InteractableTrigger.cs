using UnityEngine;

namespace Interactables.Triggers
{
	public abstract class InteractableTrigger : BaseBehaviour
	{
		[SerializeField] private Interactable interactable;

		protected void OnInteract() => interactable.Interact();
	}
}