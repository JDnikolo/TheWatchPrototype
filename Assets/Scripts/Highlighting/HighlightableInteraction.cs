using Interactables;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Interaction Highlightable")]
	public sealed class HighlightableInteraction : Highlightable
	{
		[SerializeField] private string interactActionName = "Interact";
		[SerializeField] private Interactable interactable;
		
		private InputAction m_interactAction;
		
		private void Update()
		{
			m_interactAction ??= InputManager.Instance.GetPlayerAction(interactActionName);
			if (m_interactAction.WasPressedThisFrame() && interactable) interactable.Interact();
		}

		private void OnValidate() => enabled = false;

		protected override void HighlightInternal(bool enabled) => this.enabled = enabled;
	}
}