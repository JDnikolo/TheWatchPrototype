using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Single Trigger")]
	public class InteractableSingleTrigger : InteractableTrigger
	{
		private bool m_playerEntered;
		
		private void OnTriggerEnter(Collider other)
		{
			if (m_playerEntered || !other.attachedRigidbody.gameObject.IsPlayerObject()) return;
			m_playerEntered = true;
			GameManager.Instance.InvokeOnNextFrameUpdateSafe(OnInteract);
		}
	}
}