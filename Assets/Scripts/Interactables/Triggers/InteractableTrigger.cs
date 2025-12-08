using UnityEngine;

namespace Interactables.Triggers
{
	public abstract class InteractableTrigger : BaseBehaviour
	{
		[SerializeField] private Interactable interactable;

		protected void OnInteract() => interactable.Interact();
		
		public void SetInteractable(Interactable newInteractable) => this.interactable = newInteractable;
	}
}