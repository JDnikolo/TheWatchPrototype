using System;
using Managers;
using UnityEngine;

namespace Highlighting
{
	[AddComponentMenu("Highlighting/Managed Highlightable")]
	public class ManagedHighlightable : Highlightable, IManagedHighlightable
	{
		[SerializeField] private Highlightable highlightable;
		[SerializeField] protected new Collider collider;
		[SerializeField] private float minHighlightDistance;
		[SerializeField] private float maxHighlightDistance = 3f;
		
		private Rigidbody m_rigidbody;

		public float MinHighlightDistance => minHighlightDistance;

		public float MaxHighlightDistance  => maxHighlightDistance;

		private void Start()
		{
			if (!collider) throw new Exception("Collider required for highlighting.");
			m_rigidbody = collider.attachedRigidbody;
			var highlightManager = HighlightManager.Instance;
			if (highlightManager) highlightManager.AddHighlightable(m_rigidbody, this);
		}
		
		private void OnDestroy()
		{
			if (!m_rigidbody) return;
			var highlightManager = HighlightManager.Instance;
			if (highlightManager) highlightManager.RemoveHighlightable(m_rigidbody);
		}

		protected override void HighlightInternal(bool enabled)
		{
			if (highlightable) highlightable.Highlight(enabled);
		}
	}
}