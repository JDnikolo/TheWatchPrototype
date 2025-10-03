using Managers;
using UnityEngine;

namespace Interactables
{
	[AddComponentMenu("Interactables/Toggleable Interactable")]
	public sealed class ToggleableInteractable : InteractableToggle, IHighlightableInteractable
	{
		[SerializeField] private Interactable interactable;
		[SerializeField] private new MeshRenderer renderer;

		private bool m_highlighted;
		
		public Vector3 Position => transform.position;

		public override void OnInteractionChanged(bool playerEntered)
		{
			if (playerEntered) InteractableManager.Instance.AddInteractable(this);
			else InteractableManager.Instance.RemoveInteractable(this);
		}
		
		public void Interact()
		{
			if (interactable) interactable.Interact();
		}
		
		public void Highlight(bool enabled)
		{
			if (m_highlighted == enabled) return;
			m_highlighted = enabled;
			var material = renderer.material;
			if (!material) return;
			material.SetFloat("Var_Highlight", enabled ? 1f : 0f);
		}
	}
}