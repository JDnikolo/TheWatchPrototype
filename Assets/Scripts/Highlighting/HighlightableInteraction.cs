using Interactables;
using Runtime;
using Runtime.FrameUpdate;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Interaction Highlightable")]
	public sealed class HighlightableInteraction : Highlightable, IFrameUpdatable
	{
		[SerializeField] private InputActionReference inputReference;
		[SerializeField] private Interactable interactable;

		private Updatable m_updatable;

		public FrameUpdatePosition FrameUpdateOrder => FrameUpdatePosition.Default;
		
		public void OnFrameUpdate()
		{
			if (inputReference.action.WasPressedThisFrame() && interactable) interactable.Interact();
		}

		protected override void HighlightInternal(bool enabled) => m_updatable.SetUpdating(enabled, this);
	}
}