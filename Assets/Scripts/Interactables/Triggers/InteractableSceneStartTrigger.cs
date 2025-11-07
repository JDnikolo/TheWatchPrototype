using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Interactable Scene Start Trigger")]
	public class InteractableSceneStartTrigger : MonoBehaviour
	{
		[SerializeField] private Interactable interactable;
	
		private void Start()
		{
			if (interactable) interactable.Interact();
		}
	}
}