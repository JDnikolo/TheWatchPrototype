using UnityEngine;

namespace Interactables
{
	public class HighlightableInteractable : Interactable, IHighlightable
	{
		[SerializeField] private Interactable interactable;
		[SerializeField] private new MeshRenderer renderer;
		
		public override void Interact()
		{
			throw new System.NotImplementedException();
		}

		public void Highlight(bool enabled)
		{
			var material = renderer.material;
			if (!material) return;
			material.SetFloat("Var_Highlight", enabled ? 1f : 0f);
		}
	}
}