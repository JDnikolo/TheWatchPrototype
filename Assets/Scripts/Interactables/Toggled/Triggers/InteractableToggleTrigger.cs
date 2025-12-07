using System;
using Managers.Persistent;
using UnityEngine;
using Utilities;

namespace Interactables.Toggled.Triggers
{
	[AddComponentMenu("Interactables/Triggers/Toggle Trigger")]
	public sealed class InteractableToggleTrigger : BaseBehaviour
	{
		[SerializeField] private InteractableToggle interactable;
		//TODO Replace this with a single interactable
		[Obsolete] [SerializeField] private InteractableToggle[] interactables;
		
		private bool m_playerEntered;
		private bool m_previousState;
		private bool m_sendState;

		private void CheckState()
		{
			m_sendState = true;
			if (m_previousState == m_playerEntered) return;
			m_previousState = m_playerEntered;
			interactable.OnInteractionChanged(m_playerEntered);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject()) return;
			m_playerEntered = true;
			if (m_sendState)
			{
				m_sendState = false;
				GameManager.Instance?.InvokeOnNextFrameUpdate(CheckState);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (!other.attachedRigidbody.gameObject.IsPlayerObject()) return;
			m_playerEntered = false;
			if (m_sendState)
			{
				m_sendState = false;
				GameManager.Instance?.InvokeOnNextFrameUpdate(CheckState);
			}
		}
	}
}