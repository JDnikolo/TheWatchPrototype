using UnityEngine;

namespace Interactables
{
	public sealed class InteractableToggleTrigger : MonoBehaviour
	{
		private bool m_fireOnMain;

		private void Update()
		{
			if (!m_fireOnMain) return;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player")) m_fireOnMain = true;
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.gameObject.CompareTag("Player")) m_fireOnMain = false;
		}
	}
}