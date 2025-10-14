using Highlighting.Colliders;
using Managers;
using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Managed Highlightable")]
	public sealed class ManagedHighlightable : Highlightable
	{
		[SerializeField] private Highlightable highlightable;
		[SerializeField] private ColliderGrower colliderGrower;
		[SerializeField] private float colliderGrowFactor = 1.2f;
		[SerializeField] private new Rigidbody rigidbody;
		
		private void Start()
		{
			var highlightManager = HighlightManager.Instance;
			if (highlightManager) highlightManager.AddHighlightable(rigidbody, this);
		}

		private void OnDestroy()
		{
			var highlightManager = HighlightManager.Instance;
			if (highlightManager) highlightManager.RemoveHighlightable(rigidbody);
		}

		protected override void HighlightInternal(bool enabled)
		{
			if (colliderGrower)
				if (enabled) colliderGrower.GrowCollider(colliderGrowFactor);
				else colliderGrower.ShrinkCollider();
			if (highlightable) highlightable.Highlight(enabled);
		}
	}
}