using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Toggle Trigger")]
	public sealed class InteractableToggleTrigger : MonoBehaviour
	{
		[SerializeField] private InteractableToggle[] interactables;
		
		private bool m_playerEntered;
		private bool m_previousState;

		private void CheckState()
		{
			if (m_previousState == m_playerEntered) return;
			m_previousState = m_playerEntered;
			for (var i = 0; i < interactables.Length; i++) 
				interactables[i].OnInteractionChanged(m_playerEntered);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject()) return;
			GameManager.Instance.InvokeOnNextFrameUpdateSafe(CheckState);
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject()) return;
			GameManager.Instance.InvokeOnNextFrameUpdateSafe(CheckState);
		}
	}
}