using Interactables;
using Managers.Persistent;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Interaction Highlightable")]
	public sealed class HighlightableInteraction : Highlightable, IFrameUpdatable
	{
		[SerializeField] private string interactActionName = "Interact";
		[SerializeField] private Interactable interactable;
		
		private InputAction m_interactAction;
		private Updatable m_updatable;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Default;
		
		public void OnFrameUpdate()
		{
			m_interactAction ??= InputManager.Instance.PlayerMap.GetAction(interactActionName);
			if (m_interactAction.WasPressedThisFrame() && interactable) interactable.Interact();
		}

		protected override void HighlightInternal(bool enabled) => m_updatable.SetUpdating(enabled, this);
	}
}