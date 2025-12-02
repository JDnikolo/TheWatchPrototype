using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/On Scene-Start Trigger")]
	public sealed class InteractableSceneStartTrigger : MonoBehaviour
	{
		[SerializeField] private Interactable interactable;
	
		private void Start()
		{
			if (interactable) interactable.Interact();
		}
	}
}