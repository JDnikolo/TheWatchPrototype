using UnityEngine;

namespace Interactables.Triggers
{
	public sealed class InteractableToggleTrigger : MonoBehaviour
	{
		[SerializeField] private InteractableToggle[] interactables;
		
		private bool m_playerEntered;
		private bool m_previousState;

		private void Update()
		{
			if (m_previousState == m_playerEntered) return;
			m_previousState = m_playerEntered;
			for (var i = 0; i < interactables.Length; i++) 
				interactables[i].OnInteractionChanged(m_playerEntered);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.IsPlayerObject()) m_playerEntered = true;
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.IsPlayerObject()) m_playerEntered = false;
		}
	}
}