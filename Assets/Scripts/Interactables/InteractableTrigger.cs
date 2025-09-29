using UnityEngine;

namespace Interactables
{
	public sealed class InteractableTrigger : MonoBehaviour
	{
		//[SerializeField] private Interactable[] interactables;
		[SerializeField] private Interactable interactable;

		private bool m_fireOnMain;

		private void Update()
		{
			if (!m_fireOnMain) return;
			m_fireOnMain = false;
			/*
			if (interactables != null)
				for (var i = 0; i < interactables.Length; i++)
					interactables[i].OnInteract();
			*/
			if (interactable) interactable.OnInteract();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Player")) m_fireOnMain = true;
		}
	}
}