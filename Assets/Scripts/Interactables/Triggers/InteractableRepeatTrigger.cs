using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Repeat Trigger")]
	public sealed class InteractableRepeatTrigger : InteractableTrigger
	{
		private bool m_playerEntered;
		
		private void OnTriggerEnter(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject() || m_playerEntered) return;
			m_playerEntered = true;
			GameManager.Instance?.InvokeOnNextFrameUpdate(OnInteract);
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject() || !m_playerEntered) return;
			m_playerEntered = false;
		}
	}
}