using Runtime;
using UnityEngine;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Interactable Before Play Trigger")]
	public sealed class InteractableBeforePlayTrigger : MonoBehaviour, IBeforePlay
	{
		[SerializeField] private Interactable interactable;
		
		public void OnBeforePlay()
		{
			if (interactable) interactable.Interact();
		}
	}
}