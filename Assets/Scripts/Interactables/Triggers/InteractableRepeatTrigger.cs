using UnityEngine;

namespace Interactables.Triggers
{
	public sealed class InteractableRepeatTrigger : InteractableTrigger
	{
		private bool m_playerEntered;
		
		private void Update()
		{
			if (!m_playerEntered) return;
			m_playerEntered = false;
			OnInteract();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.IsPlayerObject()) m_playerEntered = true;
		}
	}
}