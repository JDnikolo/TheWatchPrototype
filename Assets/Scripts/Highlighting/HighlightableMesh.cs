using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Mesh Highlightable")]
	public sealed class HighlightableMesh : Highlightable
	{
		[SerializeField] private new MeshRenderer renderer;
		
		protected override void HighlightInternal(bool enabled)
		{
			var material = renderer.material;
			if (!material) return;
			material.SetFloat("Var_Highlight", enabled ? 1f : 0f);
		}
	}
}