using UnityEngine;

namespace Interactables.Triggers
{
	public abstract class InteractableTrigger : MonoBehaviour
	{
		[SerializeField] private Interactable[] interactables;

		protected void OnInteract()
		{
			for (var i = 0; i < interactables.Length; i++) interactables[i].Interact();
		}
	}
}