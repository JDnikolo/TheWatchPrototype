using Managers;
using UnityEngine;
using Utilities;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Interactable Repeat Trigger")]
	public sealed class InteractableRepeatTrigger : InteractableTrigger
	{
		private void OnTriggerEnter(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject()) return;
			GameManager.Instance.InvokeOnNextFrameUpdateSafe(OnInteract);
		}
	}
}